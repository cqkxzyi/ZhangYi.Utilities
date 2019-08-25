using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace MVC3._0
{
    /// <summary>
    /// MVC扩展控件
    /// </summary>
    public static class CheckListExtensions
    {
        /// <summary>
        /// 复选框列表或单选列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控件名</param>
        /// <param name="items">列表</param>
        /// <param name="isRadio">是否是单选</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper,string name, IEnumerable<SelectListItem> items, bool isRadio = false)
        {
            #region 

            var str = new StringBuilder();
            string inputtype = "checkbox";
            string inputclass = "checkbox";
            if (isRadio)
            {
                inputtype = "radio";
                inputclass = "radiobox";
            }

            if (items == null)
            {
                return MvcHtmlString.Create("");
            }

            foreach (var item in items)
            {
                str.Append(@"<label class=""" + inputclass + @"""><input type=""");
                str.Append(inputtype);
                str.Append("\" name=\"");
                str.Append(name);
                str.Append("\" value=\"");
                str.Append(item.Value);
                str.Append("\"");

                if (item.Selected)
                    str.Append(@" checked=""chekced""");

                str.Append(" />");
                str.Append(item.Text);
                str.Append("</label>");
            }
            return MvcHtmlString.Create(str.ToString());

            #endregion
        }

        /// <summary>
        /// 复选框列表或单选列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控件名</param>
        /// <param name="items">列表</param>
        /// <param name="SelectedItems">已选中列表</param>
        /// <param name="isRadio">是否是单选</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper,string name, IEnumerable<SelectListItem> items, IEnumerable<SelectListItem> SelectedItems, bool isRadio = false)
        {
            #region 

            List<SelectListItem> itemlist = new List<SelectListItem>();
            SelectListItem additem = null;
            if (items == null)
            {
                return CheckBoxList(helper, name, itemlist);
            }
            foreach (var item in items)
            {
                if (null != SelectedItems)
                {
                    if (SelectedItems.Count(x => x.Value == item.Value) > 0)
                    {
                        additem = new SelectListItem() { Value = item.Value, Text = item.Text, Selected = true };
                    }
                    else
                    {
                        additem = new SelectListItem() { Value = item.Value, Text = item.Text, Selected = false };
                    }
                }
                else
                {
                    additem = new SelectListItem() { Value = item.Value, Text = item.Text, Selected = false };
                }
                itemlist.Add(additem);
            }

            return CheckBoxList(helper, name, itemlist, isRadio);

            #endregion
        }

        /// <summary>
        /// 复选框列表或单选列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控件名</param>
        /// <param name="items">列表</param>
        /// <param name="SelectedValue">已选中的值</param>
        /// <param name="isRadio">是否是单选</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper,string name, IEnumerable<SelectListItem> items, string SelectedValue, bool isRadio = false)
        {
            #region 

            List<SelectListItem> itemlist = new List<SelectListItem>();
            SelectListItem additem = null;
            if (items == null)
            {
                return CheckBoxList(helper, name, itemlist);
            }
            foreach (var item in items)
            {
                if (SelectedValue == item.Value)
                {
                    additem = new SelectListItem() { Value = item.Value, Text = item.Text, Selected = true };
                }
                else
                {
                    additem = new SelectListItem() { Value = item.Value, Text = item.Text, Selected = false };
                }
                itemlist.Add(additem);
            }

            return CheckBoxList(helper, name, itemlist, isRadio);

            #endregion
        }

        /// <summary>
        /// 复选框列表或单选列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控件名</param>
        /// <param name="items">列表</param>
        /// <param name="SelectedItems">已选中的值</param>
        /// <param name="isRadio">是否是单选</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper,string name, SelectList items, string SelectedValue, bool isRadio = false)
        {
            #region 

            List<SelectListItem> itemlist = new List<SelectListItem>();

            SelectListItem additem = null;
            if (items == null)
            {
                return CheckBoxList(helper, name, itemlist);
            }

            foreach (EnumItem item in items.Items)
            {

                if (SelectedValue == item.Value)
                {
                    additem = new SelectListItem() { Value = item.Value, Text = item.Description, Selected = true };
                }
                else
                {
                    additem = new SelectListItem() { Value = item.Value, Text = item.Description, Selected = false };
                }
                itemlist.Add(additem);
            }

            return CheckBoxList(helper, name, itemlist, isRadio);

            #endregion
        }



        /// <summary>
        /// 复选框列表或单选列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控件名</param>
        /// <param name="items">列表</param>
        /// <param name="SelectedItems">已选中列表</param>
        /// <param name="isRadio">是否是单选</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper,string name, SelectList items, IEnumerable<SelectListItem> SelectedItems, bool isRadio = false)
        {
            #region 
            List<SelectListItem> itemlist = new List<SelectListItem>();

            SelectListItem additem = null;
            if (items == null)
            {
                return CheckBoxList(helper, name, itemlist);
            }

            foreach (EnumItem item in items.Items)
            {
                if (null != SelectedItems)
                {
                    if (SelectedItems.Count(x => x.Value == item.Value) > 0)
                    {
                        additem = new SelectListItem() { Value = item.Value, Text = item.Description, Selected = true };
                    }
                    else
                    {
                        additem = new SelectListItem() { Value = item.Value, Text = item.Description, Selected = false };
                    }
                }
                else
                {
                    additem = new SelectListItem() { Value = item.Value, Text = item.Description, Selected = false };
                }
                itemlist.Add(additem);
            }

            return CheckBoxList(helper, name, itemlist, isRadio);

            #endregion
        }

        /// <summary>
        /// 复选框列表或单选列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控件名</param>
        /// <param name="items">列表</param>
        /// <param name="SelectedItems">已选中列表</param>
        /// <param name="isRadio">是否是单选</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, SelectList items, SelectList SelectedItems, bool isRadio = false)
        {
            #region
            List<SelectListItem> itemlist = new List<SelectListItem>();

            SelectListItem additem = null;
            if (items == null)
            {
                return CheckBoxList(helper, name, itemlist);
            }

            foreach (EnumItem item in items.Items)
            {
                if (null != SelectedItems)
                {
                    if (SelectedItems.Count(x => x.Value == item.Value) > 0)
                    {
                        additem = new SelectListItem() { Value = item.Value, Text = item.Description, Selected = true };
                    }
                    else
                    {
                        additem = new SelectListItem() { Value = item.Value, Text = item.Description, Selected = false };
                    }
                }
                else
                {
                    additem = new SelectListItem() { Value = item.Value, Text = item.Description, Selected = false };
                }
                itemlist.Add(additem);
            }

            return CheckBoxList(helper, name, itemlist, isRadio);

            #endregion
        }
    }
}
