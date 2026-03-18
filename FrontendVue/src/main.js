import { createApp } from 'vue'
import App from './App.vue'
import 'bootstrap/dist/css/bootstrap.min.css' // css bootstrap
import 'bootstrap/dist/js/bootstrap.bundle.min.js' // js bootstrap
import 'bootstrap-icons/font/bootstrap-icons.css'
import './style.css'
import router from './router'
import Toast from "vue-toastification"
import "vue-toastification/dist/index.css"

const app = createApp(App)
app.use(router)
app.use(Toast)

app.mount('#app')
