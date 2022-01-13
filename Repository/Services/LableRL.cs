using CommonLayer.Model;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class LableRL : ILableRL
    {
        readonly UserContext context;
        public LableRL(UserContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// create lable
        /// </summary>
        /// <param name="user"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool LableAdd(LabelModel user, long UserId)
        {
            try
            {              
                var Lable = this.context.UserTable.Where(x => x.UserId == UserId).SingleOrDefault();               
                Lable AddLableToNote = new Lable();             
                AddLableToNote.Lables = user.Lables;
                AddLableToNote.UserId = UserId;
                this.context.LableTable.Add(AddLableToNote);
                var result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// update note into lable or update more notes to that lable
        /// </summary>
        /// <param name="user"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool UpdateLable(LabelModel user, long UserId)
        {
            try
           {
                var userid = this.context.UserTable.Where(x => x.UserId == UserId).SingleOrDefault();
                Lable LableEntered = this.context.LableTable.FirstOrDefault(x => x.Lables == user.Lables);         
                if (LableEntered.NoteId == null)
                {
                    LableEntered.NoteId = user.NoteId;
                    this.context.SaveChanges();
                    return true;
                }
                else if (LableEntered.NoteId != null)
                {
                    Lable AddNewNote = new Lable();
                    AddNewNote.Lables = user.Lables;
                    AddNewNote.NoteId = user.NoteId;
                    AddNewNote.UserId = UserId;
                    this.context.LableTable.Add(AddNewNote);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Deletes the lable.
        /// </summary>
        /// <param name="user1">The user1.</param>
        public bool DeleteLable(LabelModel user)
        {
            try
            { 
                List<Lable> LableN= this.context.LableTable.Where(x => x.Lables == user.Lables).ToList();
                if(LableN!=null)
                {
                    foreach(var item in LableN)
                    {
                        this.context.LableTable.Remove(item);
                        this.context.SaveChanges();
                    }                  
                    return true;
                }                
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove note from Lable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RemoveNote(LabelModel user)
        {
            try
            {
                Lable LableN = this.context.LableTable.FirstOrDefault(x => x.Lables == user.Lables && x.NoteId==user.NoteId);
                if (LableN.Lables!=null)
                {
                    this.context.LableTable.Remove(LableN);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// get notes with lable name
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<Lable> GetLableRegistrations(LabelModel user)
        {
            return this.context.LableTable.Where(i => i.Lables == user.Lables).ToList();
        }
    }
}

