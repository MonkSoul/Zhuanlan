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
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Droid.Model;
using Zhuanlan.Droid.Presenter;
using Square.Picasso;
using Zhuanlan.Droid.UI.Widgets;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Zhuanlan.Droid.UI.Adapters;
using Zhuanlan.Droid.UI.Listeners;

namespace Zhuanlan.Droid.UI.Activitys
{
    [Activity(Label = "")]
    public class ColumnActivity : AppCompatActivity, View.IOnClickListener, IColumnView, SwipeRefreshLayout.IOnRefreshListener, IOnLoadMoreListener
    {
        private string slug;
        private Handler handler;
        private IColumnPresenter columnPresenter;

        private CollapsingToolbarLayout collapsingToolbar;
        private SwipeRefreshLayout swipeRefreshLayout;
        private RecyclerView recyclerView;
        private PostsAdapter adapter;
        public ImageView llAvatar;
        public TextView txtDescription;
        public TextView txtCount;
        private int limit = 10;
        private int offset = 0;
        public static void Start(Context context, string slug)
        {
            Intent intent = new Intent(context, typeof(ColumnActivity));
            intent.PutExtra("slug", slug);
            context.StartActivity(intent);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //slug = Intent.GetStringExtra("slug");
            slug = "zhihuadmin";

            handler = new Handler();
            columnPresenter = new ColumnPresenter(this);

            SetContentView(Resource.Layout.column);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            collapsingToolbar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);

            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefreshLayout.SetOnRefreshListener(this);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));

            llAvatar = FindViewById<ImageView>(Resource.Id.llAvatar);
            txtDescription = FindViewById<TextView>(Resource.Id.txtDescription);
            txtCount = FindViewById<TextView>(Resource.Id.txtCount);

            adapter = new PostsAdapter();
            adapter.OnLoadMoreListener = this;

            recyclerView.SetAdapter(adapter);
            recyclerView.Post(() =>
            {
                swipeRefreshLayout.Refreshing = true;
                OnRefresh();
            });
        }
        public void OnClick(View v)
        {
            this.Finish();
        }
        public async void OnRefresh()
        {
            await columnPresenter.GetColumn(slug);
        }

        public void GetColumnFail(string msg)
        {
            handler.Post(() =>
            {
                if (swipeRefreshLayout.Refreshing)
                {
                    swipeRefreshLayout.Refreshing = false;
                }
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            });
        }

        public void GetColumnSuccess(ColumnModel column)
        {
            handler.Post(() =>
            {
                if (swipeRefreshLayout.Refreshing)
                {
                    swipeRefreshLayout.Refreshing = false;
                }
                collapsingToolbar.SetTitle(column.Name);

                if (column.Description.Trim() == "")
                {
                    txtDescription.Visibility = ViewStates.Gone;
                }
                else
                {
                    txtDescription.Text = column.Description;
                    txtDescription.Visibility = ViewStates.Visible;
                }
                txtCount.Text = column.FollowersCount + " �˹�ע";

                var avatar = column.Avatar.Template.Replace("{id}", column.Avatar.ID);
                avatar = avatar.Replace("{size}", "l");
                Picasso.With(this)
                            .Load(avatar)
                           .Transform(new CircleTransform())
                           .Placeholder(Resource.Drawable.ic_image_placeholder)
                           .Error(Resource.Drawable.ic_image_placeholder)
                           .Into(llAvatar);
            });
        }

        public void GetPostsFail(string msg)
        {
            handler.Post(() =>
            {
                if (swipeRefreshLayout.Refreshing)
                {
                    swipeRefreshLayout.Refreshing = false;
                }
                Toast.MakeText(this, msg, ToastLength.Short).Show();
            });
        }
        public void GetPostsSuccess(List<PostModel> lists)
        {
            if (offset == 0)
            {
                handler.Post(() =>
                {
                    if (swipeRefreshLayout.Refreshing)
                    {
                        swipeRefreshLayout.Refreshing = false;
                    }
                    adapter.NewData(lists);
                    adapter.RemoveAllFooterView();
                    offset += lists.Count;
                });
            }
            else
            {
                handler.Post(() =>
                {
                    adapter.AddData(lists);
                    offset += lists.Count;
                });
            }
        }

        public async void OnLoadMoreRequested()
        {
            await columnPresenter.GetPosts(slug, offset);
        }
    }
}