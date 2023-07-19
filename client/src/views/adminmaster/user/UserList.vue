<script setup>
import { RouterLink } from 'vue-router';
import EditLayout from '../../../components/EditLayout.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import axios from 'axios';

</script>
<template>
    <EditLayout user_role="admin">
        <table class="table table-striped table-dark w-100">
            <thead>
                <tr>
                    <td>Kullanıcı adı</td>
                    <td>Email</td>
                    <td></td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="content in contentList" v-key="content.id">
                    <td>{{ content.userName }}</td>
                    <td>{{ content.email }}</td>
                    <td>
                        <button :class="'me-1 btn '+ (content.lockOut ?   'btn-success':'btn-danger')" @click="toggleLock(content)">
                            <FontAwesomeIcon :icon="content.lockOut ? 'lock-open':'lock'"></FontAwesomeIcon>
                        </button>
                        <RouterLink :to="'/AdminMaster/User/Update/'+content.id" class="btn btn-primary me-1">
                            <FontAwesomeIcon icon="pencil" />
                        </RouterLink>
                        <button class="btn btn-danger">
                            <FontAwesomeIcon icon="trash" />
                        </button> 
                    </td>
                </tr>
            </tbody>
        </table>
    </EditLayout>
</template>
<style scoped>
table img {
    height: 150px;
}

.btn_theme {
    float: right;
    color: white;
    margin-bottom: 15px;
}
</style>
<script>
export default {
    data() {
        return {
            contentList: []
        }
    },
    methods:{
        async fetchData(){
            this.contentList=(await axios.get("user/getlist")).data;
        },
        async toggleLock(content){
            content.lockOut=!content.lockOut;
            axios.get(`user/togglelock?id=${content.id}`)
        }
    },
    async mounted() {
        this.fetchData();
    }
}
</script>