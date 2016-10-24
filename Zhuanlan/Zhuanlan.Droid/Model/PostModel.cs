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
using Realms;

namespace Zhuanlan.Droid.Model
{
    public class PostModel : RealmObject
    {
        /// <summary>
        /// ���±����ͼ�Ƿ�ȫ��
        /// </summary>
        public bool IsTitleImageFullScreen { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Rating { get; set; }
        /// <summary>
        /// Դ·��
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string PublishedTime { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public LinkModel Links { get; set; }
        /// <summary>
        /// ��ר���Ĵ�������Ϣ
        /// </summary>
        public AuthorModel Author { get; set; }
        /// <summary>
        /// ������ҳ���ݻ�ȡ
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// ���±���
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ���±����ͼurl(��Ҫע�����,titleImage��ֵ�п���Ϊ��)����ͷ��һ����Ҳ������ϲ�ͬ�ĳߴ������ȡ��ͬ�ߴ��ͼƬ
        /// </summary>
        public string TitleImage { get; set; }
        /// <summary>
        /// ���¼�Ҫ��Ϣ
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// HTML��ʽ�������������飬����ͨ��WebView����UIWebViewչʾ����
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// ����״̬(�Ƿ񷢱�)
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// api�����ַ
        /// </summary>
        public string Href { get; set; }
        public MetaModel Meta { get; set; }
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        public string CommentPermission { get; set; }
        /// <summary>
        /// ����ַ?
        /// </summary>
        public string SnapshotUrl { get; set; }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool CanComment { get; set; }
        public int Slug { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int CommentsCount { get; set; }
        /// <summary>
        /// �޵�����
        /// </summary>
        public int LikesCount { get; set; }
    }
}