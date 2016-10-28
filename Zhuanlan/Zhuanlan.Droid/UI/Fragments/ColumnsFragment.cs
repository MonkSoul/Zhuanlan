using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Zhuanlan.Droid.UI.Adapters;
using Zhuanlan.Droid.UI.Listeners;
using Zhuanlan.Droid.Utils;
using Zhuanlan.Droid.Model;
using Zhuanlan.Droid.UI.Views;
using Zhuanlan.Droid.Presenter;
using Realms;
using System.Collections;

namespace Zhuanlan.Droid.UI.Fragments
{
    public class ColumnsFragment : Fragment, IColumnsView, IOnLoadMoreListener, SwipeRefreshLayout.IOnRefreshListener
    {
        private Handler handler;
        private SwipeRefreshLayout swipeRefreshLayout;
        private RecyclerView recyclerView;
        private ColumnsAdapter adapter;
        private View notLoadingView;
        private int limit = 5;
        private int offset = 0;
        private IColumnsPresenter columnsPresenter;
        private Realm realm;
        private List<int> offsetList = new List<int>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            handler = new Handler();
            realm = Realm.GetInstance();
            columnsPresenter = new ColumnsPresenter(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return inflater.Inflate(Resource.Layout.fragment_columns, container, false);
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            swipeRefreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefreshLayout.SetOnRefreshListener(this);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            adapter = new ColumnsAdapter();
            adapter.OnLoadMoreListener = this;

            recyclerView.SetAdapter(adapter);
            recyclerView.Post(() =>
            {
                swipeRefreshLayout.Refreshing = true;
                OnRefresh();
            });
        }
        private List<ColumnModel> GetColumns()
        {
            var columns = realm.All<ColumnModel>().ToList();
            var list = new List<ColumnModel>();
            var random = new Random();
            var index = -1;

            while (list.Count <= 10 && offsetList.Count < columns.Count)
            {
                do
                {
                    index = random.Next(1, columns.Count);
                }
                while (offsetList.Where(o => o == index).FirstOrDefault() > 0);
                offsetList.Add(index);
                list.Add(columns[index - 1]);
            }
            return list;
        }

        public async void OnLoadMoreRequested()
        {
            var lists = GetColumns();
            await columnsPresenter.GetColumns(lists);
        }

        public async void OnRefresh()
        {
            offsetList.Clear();
            if (offset > 0)
                offset = 0;
            var lists = GetColumns();
            if (lists.Count > 0)
            {
                await columnsPresenter.GetColumns(lists);
            }
            else
            {
                handler.Post(() =>
                {
                    if (swipeRefreshLayout.Refreshing)
                    {
                        swipeRefreshLayout.Refreshing = false;
                    }
                    adapter.LoadComplete();
                    if (notLoadingView == null)
                    {
                        notLoadingView = GetLayoutInflater(null).Inflate(Resource.Layout.recyclerview_notloading, (ViewGroup)recyclerView.Parent, false);
                    }
                    adapter.RemoveAllFooterView();
                    adapter.AddFooterView(notLoadingView);
                });
            }
        }

        public void GetColumnsFail(string msg)
        {
            handler.Post(() =>
            {
                if (offset > 0)
                {
                    adapter.ShowLoadMoreFailedView();
                }
                else
                {
                    if (swipeRefreshLayout.Refreshing)
                    {
                        swipeRefreshLayout.Refreshing = false;
                    }
                    Toast.MakeText(this.Activity, msg, ToastLength.Short).Show();
                }
            });
        }

        public void GetColumnsSuccess(List<ColumnModel> lists)
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
    }
}