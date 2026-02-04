using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoList
{
    public class MainForm : Form
    {
        private static readonly TraceSource traceSource = new TraceSource("ToDoList");
        
        private TextBox txtNewTask;
        private Button btnAdd;
        private Button btnRemove;
        private Button btnComplete;
        private ListBox lstTasks;
        private Label lblTitle;

        public MainForm()
        {
            traceSource.TraceEvent(TraceEventType.Information, 2000, "MainForm constructor called");
            InitializeComponents();
            traceSource.TraceEvent(TraceEventType.Information, 2001, "MainForm initialized successfully");
        }

        private void InitializeComponents()
        {
            // Form Einstellungen
            this.Text = "ToDo-Liste";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Titel Label
            lblTitle = new Label
            {
                Text = "Meine Aufgaben",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(450, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Textfeld für neue Aufgabe
            txtNewTask = new TextBox
            {
                Location = new Point(20, 70),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 12),
                PlaceholderText = "Neue Aufgabe eingeben..."
            };

            // Hinzufügen Button
            btnAdd = new Button
            {
                Text = "Hinzufügen",
                Location = new Point(380, 70),
                Size = new Size(90, 30),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            // ListBox für Aufgaben
            lstTasks = new ListBox
            {
                Location = new Point(20, 115),
                Size = new Size(450, 220),
                Font = new Font("Segoe UI", 11),
                SelectionMode = SelectionMode.One
            };

            // Erledigt Button
            btnComplete = new Button
            {
                Text = "Als erledigt markieren",
                Location = new Point(20, 350),
                Size = new Size(200, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(16, 124, 16),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnComplete.FlatAppearance.BorderSize = 0;
            btnComplete.Click += BtnComplete_Click;

            // Entfernen Button
            btnRemove = new Button
            {
                Text = "Löschen",
                Location = new Point(230, 350),
                Size = new Size(240, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(232, 17, 35),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += BtnRemove_Click;

            // Enter-Taste zum Hinzufügen
            txtNewTask.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    BtnAdd_Click(s, e);
                    e.Handled = true;
                }
            };

            // Steuerelemente zum Form hinzufügen
            this.Controls.Add(lblTitle);
            this.Controls.Add(txtNewTask);
            this.Controls.Add(btnAdd);
            this.Controls.Add(lstTasks);
            this.Controls.Add(btnComplete);
            this.Controls.Add(btnRemove);
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            string task = txtNewTask.Text.Trim();
            
            traceSource.TraceEvent(TraceEventType.Verbose, 3000, $"BtnAdd_Click: Attempting to add task '{task}'");
            
            if (string.IsNullOrWhiteSpace(task))
            {
                traceSource.TraceEvent(TraceEventType.Warning, 3001, "BtnAdd_Click: Empty task rejected");
                MessageBox.Show("Bitte geben Sie eine Aufgabe ein!", 
                    "Leere Eingabe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lstTasks.Items.Add("☐ " + task);
            traceSource.TraceEvent(TraceEventType.Information, 3002, $"Task added successfully: '{task}' (Total tasks: {lstTasks.Items.Count})");
            txtNewTask.Clear();
            txtNewTask.Focus();
        }

        private void BtnComplete_Click(object? sender, EventArgs e)
        {
            traceSource.TraceEvent(TraceEventType.Verbose, 4000, "BtnComplete_Click: Toggle task completion");
            
            if (lstTasks.SelectedIndex == -1)
            {
                traceSource.TraceEvent(TraceEventType.Warning, 4001, "BtnComplete_Click: No task selected");
                MessageBox.Show("Bitte wählen Sie eine Aufgabe aus!", 
                    "Keine Auswahl", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = lstTasks.SelectedIndex;
            string item = lstTasks.Items[index].ToString() ?? "";

            if (item.StartsWith("☐ "))
            {
                lstTasks.Items[index] = "☑ " + item.Substring(2) + " ✓";
                traceSource.TraceEvent(TraceEventType.Information, 4002, $"Task marked as complete at index {index}: '{item}'");
            }
            else if (item.StartsWith("☑ "))
            {
                // Zurück zu unerledigt
                string taskText = item.Substring(2);
                if (taskText.EndsWith(" ✓"))
                {
                    taskText = taskText.Substring(0, taskText.Length - 2);
                }
                lstTasks.Items[index] = "☐ " + taskText;
                traceSource.TraceEvent(TraceEventType.Information, 4003, $"Task marked as incomplete at index {index}: '{taskText}'");
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            traceSource.TraceEvent(TraceEventType.Verbose, 5000, "BtnRemove_Click: Attempting to remove task");
            
            if (lstTasks.SelectedIndex == -1)
            {
                traceSource.TraceEvent(TraceEventType.Warning, 5001, "BtnRemove_Click: No task selected");
                MessageBox.Show("Bitte wählen Sie eine Aufgabe aus!", 
                    "Keine Auswahl", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = lstTasks.SelectedIndex;
            string item = lstTasks.Items[index].ToString() ?? "";
            
            DialogResult result = MessageBox.Show(
                "Möchten Sie diese Aufgabe wirklich löschen?",
                "Aufgabe löschen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                lstTasks.Items.RemoveAt(lstTasks.SelectedIndex);
                traceSource.TraceEvent(TraceEventType.Information, 5002, $"Task removed at index {index}: '{item}' (Remaining tasks: {lstTasks.Items.Count})");
            }
            else
            {
                traceSource.TraceEvent(TraceEventType.Verbose, 5003, "BtnRemove_Click: Task removal cancelled by user");
            }
        }
    }
}
