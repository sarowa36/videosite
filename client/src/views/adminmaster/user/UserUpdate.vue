<script setup>
import axios from 'axios';
import EditLayout from '../../../components/EditLayout.vue';
import Select2 from "../../../components/Select2.vue"
import router from '../../../router';
</script>
<template>
    <EditLayout user_role="admin">
        <div class="row justify-content-end">
            <div>
                <Select2 v-model="roles" multiple>
                    <option v-for="role in allRoles" :value="role">{{ role }}</option>
                </Select2>
            </div>
            <button class="btn_theme col-2 mt-2" @click="sendRequest">Kaydet</button>
        </div>
    </EditLayout>
</template>
<script>
export default {
    data() {
        return {
            roles: null,
            allRoles: [],
        }
    },
    methods: {
        async sendRequest() {
            var data = { userId: this.$route.params.id, roles: this.roles };
            await axios.postForm("user/UpdateUserRoles", data);
            router.go(-1)
        }
    },
    async created() {
        var req = (await axios.get("user/allroles"));
        if (req == 200) {
            this.allRoles = req.data;
        }
        req = (await axios.get("user/GetUserRoles/" + this.$route.params.id));
        if (req.status == 200) {
            this.roles = req.data;
        }
    }
}
</script>