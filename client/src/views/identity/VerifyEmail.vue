<script setup >
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import axios from "axios";
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center">
            <h1 v-if="position==-1">Geçersiz Giriş İşlemi Yaptınız</h1>
            <h1 v-if="position==0">İsteğiniz İşleniyor</h1>
            <h1 v-if="position==1">Email başarıyla doğrulanmıştır lütfen önceki sekmeye geri dönün</h1>
            </div>
        </div>
</template>
<script>
export default {
    data() {
        return {
            position:0
        }
    },
    async created() {
        var result=(await axios.postForm("identity/verifyemail",{guid:this.$route.params.id})).data;
        if(result.succeeded){
            this.position=1;
        }
        else{
            this.position=-1;
        }
    }
}
</script>