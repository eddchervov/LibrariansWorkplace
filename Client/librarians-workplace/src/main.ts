import { createApp } from "vue"
import App from "./App.vue"
import router from "./router"
import store from "./store"

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"

import VueDatePicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'

const app = createApp(App).use(store).use(router)

app.component('VueDatePicker', VueDatePicker);

app.mount("#app")