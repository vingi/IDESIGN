using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace MVCBase.Common
{
    public class HtmlPageInfo : ICloneable
    {
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();
        private string _clickevent = string.Empty;
        private int _current_page = 1;
        private string _href_page = string.Empty;
        private int _index_page = 1;
        private string _mouseout = "this.style.backgroundColor='#ffffff';this.style.color='#000000';this.style.border='1px solid #cccccc';";
        private string _mouseover = "this.style.backgroundColor='#cccccc';this.style.color='#FF7F00';this.style.border='1px solid #999999';";
        private string _text = string.Empty;
        private int _width = 0x19;

        public void AddAttribute(object dictionary)
        {
            Type type = dictionary.GetType();
            if (type != null)
            {
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    string str = "";
                    str = info.GetValue(dictionary, null).ToString();
                    try
                    {
                        this._attributes.Remove(info.Name.Replace("_", "-"));
                    }
                    catch
                    {
                    }
                    this._attributes.Add(info.Name.Replace("_", "-"), str);
                }
            }
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        private string CreateStyle()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("");
            if (this._attributes.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in this._attributes)
                {
                    builder.Append(pair.Key + ":" + pair.Value);
                    builder.Append(";");
                }
            }
            return builder.ToString();
        }

        public string Render()
        {
            if (this._index_page <= 0)
            {
                this._index_page = 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<a href=\"");
            if (this._href_page.Length > 0)
                builder.Append(this._href_page + this._index_page.ToString());
            builder.Append("\" ");
            if (string.IsNullOrEmpty(this._text))
            {
                if (this._current_page == this._index_page)
                    builder.Append(" class=\"number current\" ");
                else
                    builder.Append(" class=\"number\" ");
            }
            builder.Append(this.CreateStyle());
            builder.Append(">");
            builder.Append(string.IsNullOrEmpty(this._text) ? this._index_page.ToString() : this._text);
            builder.Append("</a>");
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
                this._current_page = value;
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

        public int IndexPage
        {
            get
            {
                return this._index_page;
            }
            set
            {
                this._index_page = value;
            }
        }

        public string MouseOut
        {
            get
            {
                return this._mouseout;
            }
            set
            {
                this._mouseout = value;
            }
        }

        public string MouseOver
        {
            get
            {
                return this._mouseover;
            }
            set
            {
                this._mouseover = value;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                if (value <= 10)
                {
                    this._width = value;
                }
                else
                {
                    this._width = value;
                }
            }
        }
    }
}