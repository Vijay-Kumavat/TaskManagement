import React, { useState, useEffect } from "react";
import Header from "../components/Header";
import TaskForm from "../components/TaskForm";
import TaskList from "../components/TaskList";
import taskService from "../services/taskService";

const Home = () => {
    const [tasks, setTasks] = useState([]);
    const [selectedTask, setSelectedTask] = useState(null);
    const [isFormVisible, setIsFormVisible] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        fetchTasks();
    }, []);

    const fetchTasks = async () => {
        try {
            const response = await taskService.getTasks();
            setTasks(response.data);
        } catch (err) {
            console.error(err);
            setError("Failed to fetch tasks. Please try again later.");
        }
    };

    const addTask = async (task) => {
        try {
            const response = await taskService.addTask(task);
            setTasks([...tasks, response.data]);
            setIsFormVisible(false);
        } catch (err) {
            console.error(err);
            setError("Failed to add task. Please try again.");
        }
    };

    const updateTask = async (task) => {
        try {
            await taskService.updateTask(task.id, task);
            setTasks(tasks.map((t) => (t.id === task.id ? task : t)));
            setIsFormVisible(false);
        } catch (err) {
            console.error(err);
            setError("Failed to update task. Please try again.");
        }
    };

    const deleteTask = async (taskId) => {
        try {
            await taskService.deleteTask(taskId);
            setTasks(tasks.filter((task) => task.id !== taskId));
        } catch (err) {
            console.error(err);
            setError("Failed to delete task. Please try again.");
        }
    };

    const handleEdit = (task) => {
        if (task && task.id) {
            setSelectedTask(task);  // Only set if task has an id
            setIsFormVisible(true);
        }
    };
    
    const handleAddNew = () => {
        setSelectedTask(null);  // Reset selected task for a new task
        setIsFormVisible(true);
    };

    return (
        <div>
            <Header />
            <div className="container mt-4">
                {error && (
                    <div className="alert alert-danger" role="alert">
                        {error}
                    </div>
                )}
                {!isFormVisible ? (
                    <>
                        <button
                            className="btn btn-primary mb-3"
                            onClick={handleAddNew}
                        >
                            Add Task
                        </button>
                        <TaskList
                            tasks={tasks}
                            onDelete={deleteTask}
                            onEdit={handleEdit}
                        />
                    </>
                ) : (
                    <TaskForm
                        task={selectedTask}
                        onSubmit={selectedTask ? updateTask : addTask}
                    />
                )}
            </div>
        </div>
    );
};

export default Home;
