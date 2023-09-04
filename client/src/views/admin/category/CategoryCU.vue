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
        id:0,
        categoryName:"",
        errors
    }
},
async mounted(){
    if(this.$route.params.method.toLowerCase() == "update"){
var {data,status}=(await axios.get("category/update/"+this.$route.params.id));
if(status==200){
this.id=data.id;
this.categoryName=data.name;
}
}
},
methods:{
    async sendRequest(){
       var {data,status}= await axios.postForm("category/"+this.$route.params.method ,this.$data);
       if(status==200){
        router.go(-1)
        }
        else{
            this.errors=data;
        }
    }
}
}
</script>