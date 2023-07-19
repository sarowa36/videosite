import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue';
import Login from '../views/identity/Login.vue';
import ForgotPassword from "../views/identity/ForgotPassword.vue";
import Register from "../views/identity/Register.vue";
import Content from "../views/ContentView.vue";
import ContentCU from "../views/admin/Content/ContentCU.vue";
import ContentList from "../views/admin/Content/ContentList.vue";
import VerifyEmail from "../views/identity/VerifyEmail.vue";
import CategoryCU from "../views/admin/category/CategoryCU.vue"
import CategoryList from "../views/admin/category/CategoryList.vue"
import Notification from "../views/user/Notification.vue"
import UserList from "../views/adminmaster/user/UserList.vue"
import UserUpdate from "../views/adminmaster/user/UserUpdate.vue"

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/:val',
      name: 'index',
      component: HomeView
    },
    {
      path: '/Content/:id/:episodeId?',
      name: 'content',
      component: Content
    },
    {
      path: '/Identity/Login',
      name: 'login',
      component: Login
    },
    {
      path: '/Identity/ForgotPassword',
      name: 'forgotpassword',
      component: ForgotPassword
    },
    {
      path: '/Identity/Register',
      name: 'register',
      component: Register
    },
    {
      path: "/Identity/VerifyEmail/:id",
      name: 'verifyemail',
      component: VerifyEmail
    },
    {
      path: '/User/Notification',
      name: 'USER_notification',
      component: Notification
    },
    {
      path: '/Admin/Content/List',
      name: 'ADMIN_contentlist',
      component: ContentList
    },
    {
      path: '/Admin/Content/:method/:id?',
      name: 'ADMIN_contentCU',
      component: ContentCU
    },
    {
      path: '/Admin/Category/List',
      name: 'ADMIN_categorylist',
      component: CategoryList
    },
    {
      path: '/Admin/Category/:method/:id?',
      name: 'ADMIN_categoryCU',
      component: CategoryCU
    },
    {
      path: '/AdminMaster/User/List',
      name: 'ADMINMASTER_userlist',
      component: UserList
    },
    {
      path: '/AdminMaster/User/Update/:id',
      name: 'ADMINMASTER_UserUpdate',
      component: UserUpdate
    },
  ]
})

export default router
