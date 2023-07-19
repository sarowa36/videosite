<script setup>
import axios from 'axios';
import EditLayout from '../../../components/EditLayout.vue';
import Textbox from "../../../components/Textbox.vue"
import router from '../../../router';
</script>
<template>
    <EditLayout user_role="admin">
        <div class="row justify-content-end">
            <Textbox class="col-12" placeholder="Name" v-model="categoryName" />
            <button class="btn_theme col-2 mt-2" @click="sendRequest">Kaydet</button>
        </div>
    </EditLayout>
</template>
<script>
export default {
data(){
    return{
        categoryName:""
    }
},
methods:{
    async sendRequest(){
        await axios.postForm("category/"+this.$route.params.method ,this.$data);
        router.go(-1)
    }
}
}
</script>