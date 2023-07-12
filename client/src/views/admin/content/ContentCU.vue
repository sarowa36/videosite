<script setup>
import EditLayout from '../../../components/EditLayout.vue';
import Textbox from '../../../components/Textbox.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { Content } from "../../../models/Content"
import { Episode } from '../../../models/Episode';
import { Source } from '../../../models/Source';
import axios from "axios";

</script>
<template>
    <EditLayout user_role="admin">
        <div class="row justify-content-end">
            <Textbox class="col-12" placeholder="Name" v-model="content.name" />
            <Textbox class="col-12" placeholder="Açıklama" type="textarea" v-model="content.description" />
            <div class="mt-4 mb-2">Bölüm ve Kaynak Düzenleme</div>
            <div class="col-5">
                <ul class="episode_and_src_list">
                    <li v-for="ep in content.episodeList" :key="ep.id">
                        <div @click="(e) =>selectVal(e, ep , Episode.name)">{{ ep.name }}</div><a href="#" @click="openSrcMenu">
                            <FontAwesomeIcon icon="arrow-down" />
                        </a>
                        <ul style="display: none;">
                            <li v-for="src in ep.sourceList" @click="(e) => selectVal(e, src, Source.name)">{{ src.name }}</li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="col-2 d-flex flex-column align-center">
                <button class="btn_theme mt-1" @click="addEpisode">
                    Yeni Bölüm Ekle
                </button>
                <button v-if="this.selected.className == Episode.name" class="btn_theme mt-1" @click="addSource">
                    Yeni Kaynak Ekle
                </button>
                <button class="btn_theme mt-1" @click="removeSelected">
                    Sil
                </button>
            </div>
            <div class="col-5 list_of_src">
                <Textbox v-if="selected.name != null" v-model="selected.name" placeholder="İsim" />
                <Textbox v-if="selected.className == Source.name" v-model="selected.iframeCode" placeholder="İframe Kodu" type="textarea"
                    height="150px" />
            </div>
            <div class="mt-4"></div>
            <div class="col-4">
                <img v-if="!base64img" :src="this.API_URL+content.imageLink" alt="">
                <img v-else :src="base64img" alt="">
            </div>
            <div class="col-8">
                <Textbox type="file" accept="image/*" placeholder="Poster Yükleyiniz" v-model="base64img" id="poster"/>
            </div>
            <div class="col-2">
                <button class="btn_theme" @click="sendRequest">Kaydet</button>
            </div>  
        </div>
    </EditLayout>
</template>
<style>
.episode_and_src_list {
    border: 1px solid var(--pri-border-color);
    overflow-x: scroll;
    height: 150px;
    padding: 0;
}

.episode_and_src_list>li {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    position: relative;
}

.episode_and_src_list>li>div {
    width: 100%;
    padding-left: 13px;
}

.episode_and_src_list>li>a {
    color: var(--pri-t-color);
    text-align: center;
    position: absolute;
    right: 0;
    aspect-ratio: 1;
    width: 24px;
    height: 24px;
}

.episode_and_src_list>li>a * {
    pointer-events: none;
}

.episode_and_src_list>li>ul {
    padding: 0;
    width: 100%;
    transition: initial;
}

.episode_and_src_list>li>ul>li {
    padding-left: 25px;
}

.episode_and_src_list>li svg {
    font-size: 13px;
}

.episode_and_src_list>li> :where(div:focus, div.active, div:hover),
.episode_and_src_list ul :where(li:focus, li.active, li:hover) {
    background-color: var(--tri-color);
}
</style>
<script>
export default {
    data() {
        return {
            content: new Content,
            selected: {},
            base64img:""
        }
    },
    async created() {
        if(this.$route.params.method.toLowerCase()=="update" && this.$route.params.id){
            this.content=(await axios.get(this.API_URL+"api/content/update/"+this.$route.params.id)).data
        }
    },
    methods: {
        selectVal(event, val,className) {
            document.querySelectorAll(".episode_and_src_list .active").forEach(element => { element.classList.remove("active") });
            event.originalTarget.classList.add("active");
            this.selected = val
            this.selected.className=className;
        },
        removeSelected() { 
            if (this.selected.className== Source.name) {
                this.content.episodeList.filter((x) => x.id == this.selected.episode_id)[0].sourceList = this.content.episodeList.filter((x) => x.id == this.selected.episode_id)[0].sourceList.filter(x => x.id != this.selected.id)
            }
            else {
                this.content.episodeList = this.content.episodeList.filter(x => x.id != this.selected.id);
            }
            this.selected = {};
        },
        addEpisode() {
            var newEp = new Episode({ name: "Yeni Bölüm", sourceList: [] });
            this.content.episodeList.push(newEp);
        },
        addSource() {
            var newSrc = new Source({name: "Yeni Kaynak", episode_id: this.selected.id, iframeCode: "" });
            this.selected.sourceList.push(newSrc);
        },
        openSrcMenu(event) {
            const node = event.originalTarget;
            event.preventDefault();
            if (node.style.rotate == "") {
                node.style.rotate = "180deg"
            }
            else {
                node.style.rotate = "";
            }
            $(node.nextElementSibling).animate({ height: "toggle" });
        },
        sendRequest() {
            var data=this.content;
            data.file=document.querySelector("#poster").files[0]
            axios.postForm(this.API_URL+"api/content/"+this.$route.params.method,data)
        }
    }
}
</script>
