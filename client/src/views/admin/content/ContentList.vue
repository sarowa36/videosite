<script setup>
import { RouterLink } from 'vue-router';
import EditLayout from '../../../components/EditLayout.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { ref } from 'vue';
import { Modal } from 'flowbite-vue'
const isShowModal = ref(false)
function closeModal() {
  isShowModal.value = false
}
function showModal() {
    console.log("a")
  isShowModal.value = true
}
</script>
<template>
    <Modal size="md" v-if="isShowModal" @close="closeModal">
      <template #header>
        <div class="flex items-center text-lg">
          Terms of Service
        </div>
      </template>
      <template #body>
        <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
          With less than a month to go before the European Union enacts new consumer privacy laws for its citizens, companies around the world are updating their terms of service agreements to comply.
        </p>
        <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
          The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant to ensure a common set of data rights in the European Union. It requires organizations to notify users as soon as possible of high-risk data breaches that could personally affect them.
        </p>
      </template>
      <template #footer>
        <div class="flex justify-between">
          <button @click="closeModal" type="button" class="text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-blue-300 rounded-lg border border-gray-200 text-sm font-medium px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">
            Decline
          </button>
          <button @click="closeModal" type="button" class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
            I accept
          </button>
        </div>
      </template>
    </Modal>
    <EditLayout user_role="admin">
        <RouterLink to="/Admin/Content/Create" class="btn btn_theme">Ekle</RouterLink>
        <table class="table table-striped table-dark w-100">
            <thead>
                <tr>
                    <td></td>
                    <td>Başlık</td>
                    <td></td>
                </tr>
            </thead>
            <tbody>
                <!-- <tr>
                    <td><img src="/src/assets/poster.webp" alt=""></td>
                    <td>Accelerando Datenshi Tachi...</td>
                    <td><button class="btn btn-primary me-1">
                            <FontAwesomeIcon icon="pencil" />
                        </button>
                        <button class="btn btn-danger">
                            <FontAwesomeIcon icon="trash" />
                        </button>
                    </td>
                </tr> -->
                <tr v-for="content in contentList" v-key="content.id">
                     <td><img :src="API_URL+content.imageLink" alt=""></td>
                    <td>{{ content.name }}</td>
                    <td><RouterLink :to="'/Admin/Content/Update/'+content.id" class="btn btn-primary me-1">
                            <FontAwesomeIcon icon="pencil" />
                        </RouterLink>
                        <button class="btn btn-danger" @click="showModal">
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
            this.contentList=await $.ajax(this.API_URL+"api/Content/Getlist");
        }
    },
    async mounted() {
        this.fetchData();
    }
}
</script>