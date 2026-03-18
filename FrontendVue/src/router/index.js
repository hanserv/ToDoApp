import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import TasksView from "../views/TasksView.vue";
import UpdateTaskView from "../views/UpdateTaskView.vue";
import CreateTaskView from "../views/CreateTaskView.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/tasks',
      name: 'tasks',
      component: TasksView
    },
    {
      path: '/tasks/create',
      name: 'taskCreate',
      component: CreateTaskView
    },
    {
      path: '/tasks/update/:id',
      name: 'taskUpdate',
      component: UpdateTaskView
    }
  ]
})

export default router;