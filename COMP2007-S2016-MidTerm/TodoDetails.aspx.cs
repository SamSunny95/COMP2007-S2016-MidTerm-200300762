using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm.Models;
using System.Web.ModelBinding;
namespace COMP2007_S2016_MidTerm
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.getTodos();
            }
        }

        protected void getTodos()
        {
            // populate the form with existing todo data from the db
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to the EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // populate a todo instance with the TodoID from the URL parameter
                Todo updatedTodo = (from todos in db.Todos
                                                where todos.TodoID == TodoID
                                                select todos).FirstOrDefault();

                // map the todo properties to the form controls
                if (updatedTodo != null)
                {
                    TodoNameTextBox.Text = updatedTodo.TodoName;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                    if(updatedTodo.Completed == true)
                    {
                        CompletedCheckBox.Checked = true;
                    }
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // redirect back to todo list page
            Response.Redirect("TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // use the todos model to create a new todo object and
                // save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0)
                {
                    // get the id from url
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current todo from EF DB
                    newTodo = (from todos in db.Todos
                                     where todos.TodoID == TodoID
                                     select todos).FirstOrDefault();
                }

                // add form data to the new todo record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;


                // use LINQ to ADO.NET to add / insert new todo into the database

                // check to see if a new todo is being added
                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }

                // save our changes - run an update
                db.SaveChanges();
                    
                // Redirect back to the updated todos page
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}