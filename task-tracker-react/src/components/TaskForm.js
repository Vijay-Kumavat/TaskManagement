import React, { useState, useEffect } from "react";

const TaskForm = ({ task = {}, onSubmit }) => {
    // Initialize state with task properties or default values
    const [title, setTitle] = useState(task?.title || "");
    const [description, setDescription] = useState(task?.description || "");
    const [dueDate, setDueDate] = useState(task?.dueDate || "");
    const [status, setStatus] = useState(task?.status || "Pending");

    useEffect(() => {
        // Update state when the task prop changes (e.g., for editing)
        if (task) {
            setTitle(task.title || "");
            setDescription(task.description || "");
            setDueDate(task.dueDate || "");
            setStatus(task.status || "Pending");
        }
    }, [task]);

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!title || !description || !dueDate) {
            alert("Please fill in all fields!");
            return;
        }

        const updatedTask = { title, description, dueDate, status };
        if (task && task.id) {
            updatedTask.id = task.id; // Ensure the task ID is included for updates
        }

        onSubmit(updatedTask);
    };

    return (
        <form onSubmit={handleSubmit} className="m-auto py-4 w-50">
            <div className="mb-3">
                <label htmlFor="title" className="form-label">
                    Task Title
                </label>
                <input
                    type="text"
                    id="title"
                    className="form-control"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                />
            </div>
            <div className="mb-3">
                <label htmlFor="description" className="form-label">
                    Description
                </label>
                <textarea
                    id="description"
                    className="form-control"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                ></textarea>
            </div>
            <div className="mb-3">
                <label htmlFor="dueDate" className="form-label">
                    Due Date
                </label>
                <input
                    type="date"
                    id="dueDate"
                    className="form-control"
                    value={dueDate}
                    onChange={(e) => setDueDate(e.target.value)}
                />
            </div>
            <div className="mb-3">
                <label htmlFor="status" className="form-label">
                    Status
                </label>
                <select
                    id="status"
                    className="form-control"
                    value={status}
                    onChange={(e) => setStatus(e.target.value)}
                >
                    <option value="Pending">Pending</option>
                    <option value="In Progress">In Progress</option>
                    <option value="Completed">Completed</option>
                </select>
            </div>
            <button type="submit" className="btn btn-primary">
                {task && task.id ? "Update Task" : "Add Task"}
            </button>
        </form>
    );
};

export default TaskForm;
