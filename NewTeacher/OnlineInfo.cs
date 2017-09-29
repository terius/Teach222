﻿using Common;
using Model;
using SharedForms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewTeacher
{
    public class OnlineInfo
    {
        private IList<User> _onLineList;
        public delegate void OnlineChangeHandle(object sender, OnlineEventArgs e);
        public delegate void OnlineDelHandle(UserLogoutResponse e);
        public event OnlineChangeHandle OnLineChange;
        public event OnlineChangeHandle AddOnLine;
        public event OnlineDelHandle DelOnLine;
        public IList<User> StudentOnlineList
        {
            get
            {
                return _onLineList.Where(d => d.UserType == ClientRole.Student).ToList();
            }
        }



        public OnlineInfo()
        {
            _onLineList = new List<User>();

        }
        public IList<User> GetStudentOnlineList()
        {
            return _onLineList.Where(d => d.UserType == ClientRole.Student).ToList();
        }

        public void OnOnlineChange(IList<OnlineUserResponse> onLineList)
        {
            this._onLineList = onLineList.ConvertToUserList();
            GlobalVariable.OnlineUserList = _onLineList;
            GlobalVariable.UpdateAllTeamMemberOnline();
            //    StudentOnlineList = onLineList.Where(d => d.clientRole == ClientRole.Student).ToList();
            OnlineEventArgs e = new OnlineEventArgs(StudentOnlineList);
            OnLineChange(this, e);
        }

        public void OnNewUserLoginIn(IList<OnlineUserResponse> onLineList)
        {
            var userList = onLineList.ConvertToUserList();
            AddNewOnLine(userList[0]);
            GlobalVariable.UpdateTeamMemberOnline(userList[0].UserName, true);
            OnlineEventArgs e = new OnlineEventArgs(userList);
            AddOnLine(this, e);

        }

        public void OnUserLoginOut(UserLogoutResponse loginOutInfo)
        {
            DeleteOnLine(loginOutInfo);
            GlobalVariable.UpdateTeamMemberOnline(loginOutInfo.username, false);
            DelOnLine(loginOutInfo);

        }


        private void AddNewOnLine(User newUser)
        {
            if (!_onLineList.Any(d => d.UserName == newUser.UserName))
            {
                _onLineList.Add(newUser);
            }

            //if (!StudentOnlineList.Any(d => d.username == newUser.username))
            //{
            //    if (newUser.clientRole == ClientRole.Student)
            //    {
            //        StudentOnlineList.Add(newUser);
            //    }
            //}
        }


        private void DeleteOnLine(UserLogoutResponse user)
        {
            var item = _onLineList.FirstOrDefault(d => d.UserName == user.username);
            if (item != null)
            {
                _onLineList.Remove(item);
            }


        }
    }

    public class OnlineEventArgs : EventArgs

    {
        private IList<User> _onLineList;

        public OnlineEventArgs(IList<User> onLineList)
        {
            this._onLineList = onLineList;
        }
        public IList<User> OnLines
        {
            get { return _onLineList; }
        }


    }
}
