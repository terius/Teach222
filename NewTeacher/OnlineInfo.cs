using Common;
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
        // public delegate void OnlineChangeHandle(object sender, OnlineEventArgs e);
        // public delegate void OnlineDelHandle(UserLogoutResponse e);
        public event EventHandler<IList<User>> OnLineChange;
        public event EventHandler<IList<User>> AddOnLine;
        public event EventHandler<string> DelOnLine;
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


        public void OnOnlineChange(IList<OnlineUserResponse> onLineList)
        {
            this._onLineList = onLineList.ConvertToUserList();
            GlobalVariable.OnlineUserList = _onLineList;
            GlobalVariable.UpdateAllTeamMemberOnline();
            OnLineChange(this, StudentOnlineList);
        }

        public void OnNewUserLoginIn(IList<OnlineUserResponse> onLineList)
        {
            var userList = onLineList.ConvertToUserList();
            AddNewOnLine(userList[0]);
            GlobalVariable.UpdateTeamMemberOnline(userList[0].UserName, true);
            AddOnLine(this, userList);

        }

        public void OnUserLoginOut(UserLogoutResponse loginOutInfo)
        {
            DeleteOnLine(loginOutInfo);
            GlobalVariable.UpdateTeamMemberOnline(loginOutInfo.username, false);
            DelOnLine(this, loginOutInfo.username);

        }


        private void AddNewOnLine(User newUser)
        {
            if (!_onLineList.Any(d => d.UserName == newUser.UserName))
            {
                _onLineList.Add(newUser);
            }
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

    //public class OnlineEventArgs : EventArgs

    //{
    //    private IList<User> _onLineList;

    //    public OnlineEventArgs(IList<User> onLineList)
    //    {
    //        this._onLineList = onLineList;
    //    }
    //    public IList<User> OnLines
    //    {
    //        get { return _onLineList; }
    //    }


    //}
}
