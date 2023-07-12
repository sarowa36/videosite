import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue';
import Login from '../views/user/Login.vue';
import ForgotPassword from "../views/user/ForgotPassword.vue";
import Register from "../views/user/Register.vue";
import Content from "../views/ContentView.vue";
import Edit from "../views/user/Edit.vue";
import Index from "../views/admin/Index.vue";
import ContentCU from "../views/admin/Content/ContentCU.vue";
import ContentList from "../views/admin/Content/ContentList.vue";

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
      path:'/User/Login',
      name:'login',
      component:Login
    },
    {
      path:'/User/ForgotPassword',
      name:'forgotpassword',
      component:ForgotPassword
    },
    {
      path:'/User/Register',
      name:'register',
      component:Register
    },
    {
      path:'/User/Edit',
      name:'edit',
      component:Edit
    },
    {
      path:'/Admin/Index',
      name:'admin_index',
      component:Index
    },
    {
      path:'/Admin/Content/List',
      name:'admin_contentlist',
      component:ContentList
    },
    {
      path:'/Admin/Content/:method/:id?',
      name:'admin_contentCU',
      component:ContentCU
    }
  ]
})

export default router
