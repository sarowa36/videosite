<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import Textbox from '../../components/Textbox.vue';
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center">
            <form v-if="!successRefresh" @submit="() => { this.successRefresh = true }"
                class="form col-md-9 col-lg-6 d-flex flex-column">
                <Textbox placeholder="Kullanıcı Adı" id="UserId" />
                <button class="submit_btn mt-3 mb-3" type="submit">Şifreyi Yenile</button>
            </form>
            <p v-if="successRefresh" class="form col-md-9 col-lg-6">Şifre Yenileme İşleminiz Başarıyla Tamamlanmıştır</p>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            successRefresh: false
        }
    }
}
</script>