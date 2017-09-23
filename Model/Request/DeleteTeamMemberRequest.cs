namespace Model
{
    public class DeleteTeamMemberRequest :IsendRequest
    {
        /// <summary>
        /// 群组ID
        /// </summary>
        public string TeamId { get; set; }



        /// <summary>
        /// 被删除的用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否删除此群组
        /// </summary>
        public bool IsDeleteTeam { get; set; }
    }
}
