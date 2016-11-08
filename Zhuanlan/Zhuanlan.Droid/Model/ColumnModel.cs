using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Zhuanlan.Droid.Model
{
    public class ColumnModel 
    {
        /// <summary>
        /// ��ע��
        /// </summary>
        public int FollowersCount { get; set; }
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public AuthorModel Creator { get; set; }
        /// <summary>
        /// ר��������Ϣ
        /// </summary>
        public IList<TopicModel> Topics { get; }
        /// <summary>
        /// ����״̬
        /// </summary>
        public string ActivateState { get; set; }
        /// <summary>
        /// ר������
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// �Ƿ�����ύ����
        /// </summary>
        public bool AcceptSubmission { get; set; }
        public bool FirstTime { get; set; }
        /// <summary>
        /// ���»�����Ϣ
        /// </summary>
        public IList<PostTopicModel> PostTopics { get; }
        public string PendingName { get; set; }
        public IList<PostTopicModel> PendingTopics { get; }
        /// <summary>
        /// ͷ��
        /// </summary>
        public AvatarModel Avatar { get; set; }
        /// <summary>
        /// �Ƿ����Ա
        /// </summary>
        public bool CanManage { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ����һ���޸����Ƶ�ʱ��
        /// </summary>
        public int NameCanEditUntil { get; set; }
        /// <summary>
        /// �������ԭ��
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// �������ʱ��
        /// </summary>
        public int BanUntil { get; set; }
        /// <summary>
        /// ������ַ
        /// </summary>
        public string Slug { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// ר��������Ϣ
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// ����һ���޸Ļ����ʱ��
        /// </summary>
        public int TopicsCanEditUntil { get; set; }
        public string ActivateAuthorRequested { get; set; }
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        public string CommentPermission { get; set; }
        /// <summary>
        /// �Ƿ��ע��ר��
        /// </summary>
        public bool Following { get; set; }
        /// <summary>
        /// ר��������
        /// </summary>
        public int PostsCount { get; set; }
        /// <summary>
        /// ��ǰ�ʺ��Ƿ��з������µ�Ȩ��
        /// </summary>
        public bool CanPost { get; set; }
    }
}