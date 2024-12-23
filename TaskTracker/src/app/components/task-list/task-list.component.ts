import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Task } from '../../models/Task';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css'],
  imports: [
    CommonModule,
    RouterLink
  ]
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks().subscribe((data) => (this.tasks = data));
  }

  // Function to show the delete confirmation alert
  deleteTask(id: number): void {
    // Display a native JavaScript confirmation popup
    const confirmed = window.confirm('Are you sure you want to delete this task?');
    
    if (confirmed) {
      // If the user confirms, delete the task
      this.taskService.deleteTask(id).subscribe(() => {
        this.loadTasks(); // Reload tasks after deletion
      });
    }
  }
}
