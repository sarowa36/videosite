<script setup>
import axios from 'axios';
import { RouterLink } from 'vue-router';
</script>
<template>
    <div class="container content mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-3"><img class="profile_img" :src="inspectUser.imageLink" alt=""></div>
            <div class="col-12 text-center">
                <h3>{{inspectUser.userName}}</h3>
                <RouterLink v-if="USER.name!=inspectUser.userName" class="btn btn_theme" :to="'/user/messages/'+inspectUser.id">Mesaj Gönder
                </RouterLink>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            inspectUser:{
                userName:"",
                imageLink:"",
                id:""
            }
        }
    },
    async created(){
        var id=this.$route.params.id;
        if(id!=null && id){
            var req=(await axios.get("usercommunication/getUser/"+id));
            if(req.status==200){
        this.inspectUser=req.data
    }
    }
    }
}
</script>
<style scoped>
.profile_img {
    aspect-ratio: 1;
    border-radius: 50%;
    object-fit: cover;
    width: 100%;
}
</style>