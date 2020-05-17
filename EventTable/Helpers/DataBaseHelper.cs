using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using EventTable.Data;
using EventTable.Models.Entities;
using Npgsql;

namespace EventTable.Helpers
{
    /// <summary>
    /// Подключение к базе данных
    /// </summary>
    public class DataBaseHelper
    {
        private ApplicationDbContext db { get; set; }

        public DataBaseHelper()
        {
            this.db = new ApplicationDbContext();
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        public void AddUser(User user)
        {
            try 
            {
                if (!this.UserExist(user.ChatId))
                {
                    this.db.Add<User>(user);
                    this.db.SaveChanges();
                }
                
            }
            catch(Exception e) 
            {
                throw e;
            }
        }
        /// <summary>
        /// Получение User по chatId
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public User GetUser(long chatId)
        {
            try
            {
                return this.db.Users.Where(u => u.ChatId == chatId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Проверка существования Пользователя 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UserExist(long chatId)
        {
            try
            {
                var user = this.db.Users.FirstOrDefault(u => u.ChatId == chatId);
                return user != null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Добавление Event, определение владельца - автоматически
        /// </summary>
        /// <param name="event"></param>
        /// <param name="chatId"></param>
        public void AddEvent(Event @event, long chatId)
        {
            try
            {
                var user = this.GetUser(chatId);
                //если нет ивента, то создатель - владелец
                if (this.db.Events.Where(x => x.Id == @event.Id).FirstOrDefault() == null)
                {
                    this.db.Add<Event>(@event);
                    this.db.Add<UserEvents>(new UserEvents() 
                    {
                        EventId = @event.Id,
                        UserId = user.Id,
                        IsOwner = 1 
                    });
                }
                else
                {
                    this.db.Add<UserEvents>(new UserEvents()
                    {
                        EventId = @event.Id,
                        UserId = user.Id,
                        IsOwner = 0
                    });
                }
                this.db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e; 
            }
        }
        /// <summary>
        /// Удаление события
        /// </summary>
        /// <param name="event"></param>
        /// <param name="chatId"></param>
        public void RemoveEvent(Event @event, long chatId)
        {
            try
            {
                var user = this.GetUser(chatId);
                var userEvent = this.db.UserEvents.Where(ue => ue.EventId == @event.Id && ue.UserId == user.Id).FirstOrDefault();

                //если удаляет владелец, то событие удаляется абсолютно у всех, а потом и само событие,
                //Иначе удалятся запись пользователя на событие
                if (userEvent.IsOwner == 1)
                {
                    var events = this.db.UserEvents.Where(ue => ue.EventId == @event.Id).ToList();
                    this.db.UserEvents.RemoveRange(events);
                    this.db.Events.Remove(@event);
                }
                else
                {
                    this.db.UserEvents.Remove(userEvent);
                }
                this.db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Получение списка всех мероприятий(1 - которые ораганизовал, 0 - на которые подписан)
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="isOwner"></param>
        /// <returns></returns>
        public List<Event> GetUserEvents(long chatId, int isOwner)
        {
            try
            {
                var user = this.GetUser(chatId);
                return this.db.UserEvents
                .Where(ue => ue.UserId == user.Id && ue.IsOwner == isOwner)//или ChatId что тебе легче передавать? но если у тебя будет user, то пофиг
                .Join(this.db.Events, ue => ue.EventId, e => e.Id,
                (ue, ev) => new Event()
                {
                    Id = ev.Id,
                    Description = ev.Description,
                    Name = ev.Name,
                    Place = ev.Name,
                    Happen_date = ev.Happen_date,
                    Notify_date = ev.Notify_date
                }).Where(e=>e.Happen_date>DateTime.Now)
                .ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Получение всех публичных Events
        /// </summary>
        /// <returns></returns>
        public List<Event> GetAllEvents()
        {
            try
            {
                return db.Events
                    .Where(ev=>ev.Happen_date>DateTime.Now)
                    .ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        ///  Получение всех участиков по id event'а
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public List<User> GetEventSubs(int eventId)
        {
            try
            {
                return db.UserEvents
                    .Where(ue=>ue.EventId == eventId && ue.IsOwner == 0)
                    .Join(db.Users, ue => ue.UserId, u => u.Id,
                    (ue, u) => new User()
                    {
                        ChatId = u.ChatId,
                        Id = u.ChatId,
                        Login = u.Login,
                        Notes = u.Notes
                    })
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
