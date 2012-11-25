using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace MVCBase.Common
{
    public class HtmlPagerControl
    {
        private string _clickevent;
        private int _current_page = 1;
        private int _display_count = 5;
        private int _display_page_count;
        private string _href_page = string.Empty;
        private List<HtmlPageInfo> _items = new List<HtmlPageInfo>();
        private int _left_display_number;
        private string _middle_seperator = "...";
        private int _page_count;
        private int _right_display_number;
        private int _step;
        private int _totalcount;
        private string _totalpageid = string.Empty;

        public HtmlPagerControl(int pageCount, int display_page_count, int totalcount)
        {
            this._page_count = pageCount;
            this._display_page_count = display_page_count;
            this._totalcount = totalcount;
            this._step = display_page_count / 2;
        }

        public void Add(HtmlPageInfo info)
        {
            try
            {
                info.CurrentPage = this._current_page;
                this._items.Add(info);
            }
            catch
            {
                throw new Exception("类型不匹配");
            }
        }

        private void Init()
        {
            HtmlPageInfo info;
            HtmlPageInfo info2;
            HtmlPageInfo info3;
            HtmlPageInfo info4;
            if ((this._current_page - this._step) < 1)
            {
                this._left_display_number = 1;
                this._right_display_number = (this._left_display_number + this._display_page_count) - 1;
            }
            else
            {
                this._left_display_number = this._current_page - this._step;
                this._right_display_number = this._current_page + this._step;
            }
            if (this._right_display_number >= this._page_count)
            {
                this._right_display_number = this._page_count;
                this._left_display_number = ((this._right_display_number - this._display_page_count) <= 0) ? 1 : (this._right_display_number - (this._display_page_count - 1));
            }
            if ((this._right_display_number - this._left_display_number) == this._display_page_count)
            {
                this._right_display_number--;
            }
            if (this._current_page > 1)
            {
                info = new HtmlPageInfo();
                info.CurrentPage = this._current_page;
                info.HrefPage = this._href_page;
                info.ClickEvent = this._clickevent;
                info.IndexPage = 1;
                info.Text = "&laquo; 首頁";
                info.Width = 50;
                info2 = new HtmlPageInfo();
                info2.CurrentPage = this._current_page;
                info2.IndexPage = this._current_page - 1;
                info2.HrefPage = this._href_page;
                info2.ClickEvent = this._clickevent;
                info2.Text = "&laquo; 上一頁";
                info2.Width = 50;
            }
            else
            {
                info = null;
                info2 = null;
            }
            if (this._current_page < this._page_count)
            {
                info3 = new HtmlPageInfo();
                info3.CurrentPage = this._current_page;
                info3.HrefPage = this._href_page;
                info3.ClickEvent = this._clickevent;
                info3.IndexPage = this._current_page + 1;
                info3.Text = "下一頁 &raquo;";
                info3.Width = 50;
                info4 = new HtmlPageInfo();
                info4.CurrentPage = this._current_page;
                info4.HrefPage = this._href_page;
                info4.ClickEvent = this._clickevent;
                info4.IndexPage = this._page_count;
                info4.Text = "末頁 &raquo;";
                info4.Width = 50;
            }
            else
            {
                info4 = null;
                info3 = null;
            }
            if (info != null)
            {
                this._items.Add(info);
            }
            if (info2 != null)
            {
                this._items.Add(info2);
            }
            for (int i = this._left_display_number; i <= this._right_display_number; i++)
            {
                HtmlPageInfo item = new HtmlPageInfo();
                item.IndexPage = i;
                if (this._href_page.EndsWith("/"))
                {
                    this._href_page.Remove(this._href_page.Length - 1, 1);
                }
                item.HrefPage = this._href_page;
                item.ClickEvent = this._clickevent;
                item.CurrentPage = this._current_page;
                item.IndexPage = i;
                this._items.Add(item);
            }
            if (info3 != null)
            {
                this._items.Add(info3);
            }
            if (info4 != null)
            {
                this._items.Add(info4);
            }
        }

        private List<int> ListPage(int current, int place, bool reverse)
        {
            List<int> list = new List<int>();
            while (current != place)
            {
                list.Add(current);
                if (reverse)
                {
                    current++;
                }
                else
                {
                    current--;
                }
            }
            return list;
        }

        public void RemoveAll()
        {
            this._items.Clear();
        }

        public string Render()
        {
            this.Init();
            StringBuilder builder = new StringBuilder();
            if (this._page_count > 1)
            {
                builder.Append("<div style=\"text-align: center;margin: 10px;font-family: 黑体;color: #57A000;\"><div style=\"float: left;\">總 " + this._totalcount.ToString() + " 筆</div><div style=\"float: right;\">共 <span id=\"" + this._totalpageid + "\">" + this._page_count.ToString() + "</span> 頁</div>");
                foreach (HtmlPageInfo info in this._items)
                {
                    builder.Append(info.Render());
                }
            }
            else
            {
                builder.Append("<div style=\"text-align: center;margin: 10px;color: #57A000;\"><div style=\"float: left;\">總 " + this._totalcount.ToString() + " 筆</div><div style=\"float: right;\">共 <span id=\"" + this._totalpageid + "\">" + this._page_count.ToString() + "</span> 頁</div>");
            }
            builder.Append("<div style=\"clear: both;\"></div></div>");
            return builder.ToString();
        }

        public string ClickEvent
        {
            get
            {
                return this._clickevent;
            }
            set
            {
                this._clickevent = value;
            }
        }

        public int CurrentPage
        {
            get
            {
                return this._current_page;
            }
            set
            {
                if (value > this._page_count)
                {
                    this._current_page = this._page_count;
                }
                else if (value < 1)
                {
                    this._current_page = 1;
                }
                else
                {
                    this._current_page = value;
                }
            }
        }

        public int Displaycount
        {
            get
            {
                return this._display_count;
            }
            set
            {
                this._display_count = value;
            }
        }

        public string HrefPage
        {
            get
            {
                return this._href_page;
            }
            set
            {
                this._href_page = value;
            }
        }

        public HtmlPageInfo this[int index]
        {
            get
            {
                if ((index >= this._page_count) || (index < 0))
                {
                    throw new Exception("索引超出大小");
                }
                return this._items[index];
            }
        }

        public List<HtmlPageInfo> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }

        public int left_display_number
        {
            get
            {
                return this._left_display_number;
            }
        }

        public string MiddleSeperator
        {
            get
            {
                return this._middle_seperator;
            }
            set
            {
                this._middle_seperator = value;
            }
        }

        public int PageCount
        {
            get
            {
                return this._page_count;
            }
        }

        public int right_display_number
        {
            get
            {
                return this._right_display_number;
            }
        }

        public int step
        {
            get
            {
                return this._step;
            }
        }

        public string TotalPageId
        {
            get
            {
                return this._totalpageid;
            }
            set
            {
                this._totalpageid = value;
            }
        }
    }
}