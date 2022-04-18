using Common.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Sample.DataLayer;

// 09/23/2020 12:47 pm - SSN - [20200923-1247] - [001] - M08 - A design pattern for creating master / detail screen

namespace WPF.Sample.ViewModelLayer
{
    public class UserMaintenanceDetailViewModel : UserMaintenanceListViewModel
    {

        private User _Entity = new User();
        private User _OriginalEntity = new User();

        public User Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
                IsRecordSelectedViewOnly = _Entity != null;

            }
        }


        public override void LoadUsers()
        {
            base.LoadUsers();

            if (Users.Count > 0)
            {
                Entity = Users[0];
            }

        }


        public override void BeginEdit(bool isAddMode = false)
        {
            Clone<User>(Entity, _OriginalEntity);

            if (isAddMode)
            {
                Entity = new User();
            }

            base.BeginEdit(isAddMode);

        }

        public override void CancelEdit()
        {
            base.CancelEdit();

            Clone<User>(_OriginalEntity, Entity);


            // 03/30/2022 12:10 PM - ssn
            // tODO
            _OriginalEntity = new User();


        }

        public override bool Save()
        {

            bool ret = false;


            SampleDbContext db = null;

            try
            {
                db = new SampleDbContext();

                if (IsAddMode)
                {
                    Entity.Password = StringHelper.CreateRandomString(16);
                    db.Users.Add(Entity);
                }
                else
                {
                    db.Entry(Entity).State = EntityState.Modified;
                }

                db.SaveChanges();

                ret = true;

                _OriginalEntity = Entity;

                if (IsAddMode)
                {
                    Users.Add(Entity);
                }

                CancelEdit();

            }
            catch (DbEntityValidationException ex)
            {
                ValidationMessages = new ObservableCollection<ValidationMessage>(db.CreateValidationMessages(ex));
                IsValidationVisible = true;

                APP_INSIGHTS.ai.TrackException("ps-253-20220416-0019 - User maintenance - Save failed Db validation", ex);

            }
            catch (Exception ex)
            {
                PublishException(ex);
            }

            return ret;

        }

        public override bool Delete()
        {
            bool ret = false;


            int index = 0;
            SampleDbContext db = null;

            User entity;

            try
            {
                db = new SampleDbContext();

                entity = db.Users.Find(Entity.UserId);

                if (entity != null)
                {
                    index = db.Users.ToList().IndexOf(entity);
                    db.Users.Remove(entity);
                    db.SaveChanges();

                    ret = true;

                    Users.Remove(Entity);

                    if (Users.Count > 0)
                    {
                        index++;
                        if (index > Users.Count)
                        {
                            index = Users.Count - 1;
                        }
                        Entity = Users[index];
                    }
                    else
                    {
                        Entity = null;
                    }

                }
            }
            catch (DbEntityValidationException ex)
            {
                ValidationMessages = new ObservableCollection<ValidationMessage>(db.CreateValidationMessages(ex));
                IsValidationVisible = true;
                APP_INSIGHTS.ai.TrackException("ps-253-2022041-0018: User maintenance - DB validation error", ex);

            }
            catch (Exception ex)
            {
                PublishException(ex);
            }

            return ret;


        }


    }


}
