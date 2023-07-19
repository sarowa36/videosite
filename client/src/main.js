import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import jQuery from 'jquery'
import select2 from "select2/dist/js/select2.full.min.js";
import axios from "axios";

import 'bootstrap/dist/css/bootstrap.min.css';
import "./assets/main.css"
import "select2/dist/css/select2.min.css";
import "jquery-ui/dist/themes/ui-darkness/theme.css"
import "jquery-ui/dist/themes/ui-darkness/jquery-ui.min.css"

(async ()=>{
   const app = createApp(App)
   //app.config.globalProperties.API_URL="https://localhost:7292/";
   //axios.defaults.baseURL="https://localhost:7292/api/";
   app.config.globalProperties.API_URL=location.origin+"/";
   axios.defaults.baseURL=location.origin+"/api/";
   
   var user=(await axios.get("identity/getuser")).data;
   app.config.globalProperties.USER=user;
   app.config.globalProperties.isAdmin=()=>{
     return user && user.roles.includes("ADMIN");
   }
   app.config.globalProperties.isAdminMaster=()=>{
    return user && user.roles.includes("ADMIN_MASTER");
  }
   app.config.globalProperties.isUser=()=>{
    return user && user.roles.includes("USER");
  }
  app.config.globalProperties.checkRole=(str)=>{
    return user.roles?.includes(str) ?? false;
  }
    window.$ = window.jQuery = jQuery;

    import("jquery-ui/dist/jquery-ui")
    select2();
    /* region font awasome */
    library.add(fas);
    library.add(far);
    app.component('font-awesome-icon',FontAwesomeIcon);
    /* endregion font awasome */

    app.use(router);
    
    app.mount('#app')
})()