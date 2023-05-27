import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import jQuery from 'jquery'

import 'bootstrap/dist/css/bootstrap.min.css';
import "./assets/main.css"
const app = createApp(App)

window.$ = window.jQuery = jQuery;

/* region font awasome */
library.add(fas);
library.add(far);
app.component('font-awesome-icon',FontAwesomeIcon);
/* endregion font awasome */

app.use(router);

app.mount('#app')