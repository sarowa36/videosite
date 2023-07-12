<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import Textbox from "../components/Textbox.vue"
import { Comment } from "../models/Comment";
import { Content } from "../models/Content";
import axios from "axios";
import { Episode } from "../models/Episode";
import {RouterLink} from "vue-router";
import { watch } from "vue";
</script>
<template>
    <div class="container content mt-5 mb-5">
        <div class="row">
            <div class="col-12 mb-2">
                <div class="content_title_parent">
                    <h3 class="m-0">{{content.name}}</h3>
                </div>
            </div>
            <div class="col-lg-8 tab_content">
                <div v-for="(src, i) in currentEpisode.sourceList" :id="'src'+src.id" :class="i==0 ? 'tab':'tab d-none'" >
                    <iframe :src="src.iframeCode" title="YouTube video player" frameborder="0"></iframe>
                </div>
                <div class="col-12 mt-3">
                    <div class="tabs_label">
                        Kaynaklar:
                       <label v-for="(src,i) in currentEpisode.sourceList" :for="'src'+src.id" :class="i==0?'focus':''" @click="(e) =>changeLabel(e,src)">{{ src.name }}</label> 
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <ul class="list_of_episode p-0" style="height: 400px;">
                    <li v-for="ep in content.episodeList"><RouterLink :to="`/Content/${content.id}/${ep.id}`">{{ ep.name }}</RouterLink></li>
                </ul>
            </div>

            <div class="col-12 mt-4">
                <div class="content_title_parent">
                    <h3 class="m-0">Açıklama</h3>
                </div>
            </div>
            <img :src="this.API_URL+content.imageLink" class="col-lg-2 mt-2 poster" alt="">
            <div class="col-lg-8 mt-2">
                <div class="list_of_content_values mb-2">
                    <button class="btn_theme">
                        <FontAwesomeIcon icon="eye" /> 300
                    </button>
                    <button class="btn_theme">
                        <FontAwesomeIcon icon="comments" /> 300
                    </button>
                    <button class="btn_theme">
                        <FontAwesomeIcon icon="thumbs-up" /> 300
                    </button>
                    <button class="btn_theme">
                        <FontAwesomeIcon icon="tag"></FontAwesomeIcon> Horror
                    </button>
                    <button class="btn_theme">
                        <FontAwesomeIcon icon="tag"></FontAwesomeIcon> Comedy
                    </button>
                </div>
                <div class="content_description">
                    {{content.description}}
                </div>
            </div>
            <div class="col-12 mt-4">
                <div class="content_title_parent">
                    <h3 class="m-0">Yorumlar</h3>
                </div>
                <div class="comment">
                    <div class="create_comment"></div>
                    <div class="list_of_comment">
                        <div class="on_writing_comment">
                            <img src="../assets/poster.webp" alt="">
                            <Textbox class="content_text" placeholder="Yorum Giriniz" />
                            <div class="w-100 text-end">
                                <button class="submit_btn mt-2">Gönder</button>
                            </div>
                            <div v-if="!login" class="login_blur"> <button class="btn_theme" @click="login = true">Giriş
                                    Yap</button></div>
                        </div>
                        <div v-for="i in comments">
                            <img :src="i.img" alt="">
                            <span v-if="!i.isOverflow" class="content_text">
                                {{ i.text }}
                            </span>
                            <span v-else class="content_text">
                                {{ i.text.slice(0, 350) }}
                                <a href="#" @click="(e) => { e.preventDefault(); i.isOverflow = false; }"
                                    class="readmore">Devamını Okuyun</a>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            comments: [],
            content:new Content(),
            currentEpisode:new Episode({}),
            login: false
        }
    },
    methods: {
        resizeEpisodeNode() {
            var doit;
            var resizedw = () => {
                document.querySelector(".list_of_episode").style.height = document.querySelector(".content iframe").offsetHeight + "px";
            }
            resizedw()
            window.onresize = function () {
                clearTimeout(doit);
                doit = setTimeout(resizedw, 100);
            };
        },
        changeLabel(e,src){
            $(".tabs_label > label").removeClass("focus");
            e.target.classList.add("focus")
            $(".tab_content > .tab").addClass("d-none")
            $(`.tab_content > #src${src.id}.tab`).removeClass("d-none")
        },
        async refreshModel(){
            this.content=(await axios.get(this.API_URL+`api/Content/Get/${this.$route.params.id}`)).data;
        this.currentEpisode= this.$route.params.episodeId ? this.content.episodeList.filter(x=>x.id== this.$route.params.episodeId)[0] : this.content.episodeList[0];
        (await $.ajax("/src/jsonexamples/Comments.json")).forEach(i => this.comments.push(new Comment(i)));
        console.log(1)
        }
    },
    async created() {
        this.refreshModel();
    },
    watch:{
        $route(from,to){
            this.refreshModel();
        }
    }
}
</script>
<style scoped>
.list_of_comment>div {
    display: flex;
    align-items: start;
    justify-content: space-between;
    flex-wrap: wrap;
    padding: 13px 2px;
}

.list_of_comment>div.on_writing_comment {
    align-items: center;
    position: relative;
}

.list_of_comment>div.on_writing_comment>.login_blur {
    position: absolute;
    width: 100%;
    height: 100%;
    backdrop-filter: blur(3px);
    box-shadow: 0px 0px 5px rgba(211, 211, 211, .1);
    display: flex;
    justify-content: center;
    align-items: center;
}

.list_of_comment>div>img {
    width: 50px;
    height: 50px;
    object-fit: cover;
    border-radius: 100%;
    margin: 5px;
}

.list_of_comment>div>.content_text {
    width: calc(100% - 70px);
}

.readmore {
    color: var(--pri-btn-color);
    height: 24px;
    display: inline-block;
    position: relative;
    right: 35px;
}

.readmore::before {
    background: linear-gradient(to right, rgba(0, 0, 0, 0), rgb(0, 0, 0));
    content: '\00a0';
    width: 31px;
    display: inline-block;
}

.poster {
    aspect-ratio: 2/3;
    object-fit: contain;
}

.content_title_parent {
    border-bottom: 1px solid var(--pri-border-color);
}

.content_title_parent>h3 {
    border-bottom: 1px solid var(--pri-border-active-color);
    width: fit-content;
}

.list_of_content_values {
    display: flex;
    column-gap: 15px;
}


.content iframe {
    width: 100%;
    aspect-ratio: 16/9;
}

.list_of_episode {
    overflow-y: scroll;
    max-height: 100%;
}

.list_of_episode>li>a {
    color: var(--pri-t-color);
    width: 100%;
    display: block;
    padding: 5px 0;
    border-block: 1px solid var(--pri-border-color);
}

.list_of_episode>li:first-child>a {
    border-top: initial;
}


.tab_input:checked+div.tab {
    display: block;
}

.tabs_label>label {
    padding: 5px;
    border: 1px solid var(--pri-border-color);
    margin-right: 10px;
    cursor: pointer;
    color: var(--sec-btn-active-color);
    background-color: var(--sec-btn-color);
    border-radius: 3px;
}

.tabs_label>label:focus,
.tabs_label>label.focus,
.tabs_label>label:hover {
    background-color: var(--sec-btn-active-color);
    color: var(--sec-btn-color);
}

@media (max-width:992px) {
    .poster {
        max-height: 450px;
    }
}</style>