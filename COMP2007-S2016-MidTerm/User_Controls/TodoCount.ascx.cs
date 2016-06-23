using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm.User_Controls
{
    public partial class TodoCount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getCount();
        }

        protected void getCount()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                // query the Todos Table using EF and LINQ
                var Todos = (from allTodos in db.Todos
                             select allTodos);

                int Count = Todos.Count();
                TodoCountLabel.Text = Count.ToString();

            }
        }
    }
}