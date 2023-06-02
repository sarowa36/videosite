import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue';
import Login from '../views/user/Login.vue';
import ForgotPassword from "../views/user/ForgotPassword.vue";
import Register from "../views/user/Register.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/Index',
      name: 'index',
      component: HomeView
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
    }
    ,
    {
      path:'/User/Register',
      name:'register',
      component:Register
    }
  ]
})

export default router
