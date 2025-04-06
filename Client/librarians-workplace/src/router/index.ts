import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/reader-management',
    name: 'ReaderManagement',
    component: () => import('../views/ReaderManagementView.vue')
  },
  {
    path: '/books',
    name: 'books',
    component: () => import('../views/BooksView.vue')
  },
  {
    path: '/book-management',
    name: 'BookManagement',
    component: () => import('../views/BookManagementView.vue')
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
