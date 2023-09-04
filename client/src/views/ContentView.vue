<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import Textbox from "../components/Textbox.vue"
import { Comment } from "../models/Comment";
import { Content } from "../models/Content";
import axios from "axios";
import { Episode } from "../models/Episode";
import { RouterLink } from "vue-router";
import router from "../router/index";
import { watch } from "vue";
</script>
<template>
    <div class="container content mt-5 mb-5">
        <div class="row">
            <div class="col-12 mb-2">
                <div class="content_title_parent">
                    <h3 class="m-0">{{ content.name }}</h3>
                </div>
            </div>
            <div ref="tabcontent" class="col-lg-8 tab_content">
                <div v-for="(src, i) in currentEpisode.sourceList" :id="'src' + src.id"
                    :class="i == 0 ? 'tab' : 'tab d-none'">
                    <iframe :src="src.iframeCode" title="YouTube video player" frameborder="0"></iframe>
                </div>
                <div class="col-12 mt-3">
                    <div class="tabs_label">
                        Kaynaklar:
                        <label v-for="(src, i) in currentEpisode.sourceList" :for="'src' + src.id"
                            :class="i == 0 ? 'focus' : ''" @click="(e) => changeLabel(e, src)">{{ src.name }}</label>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <ul class="list_of_episode p-0" style="height: 400px;">
                    <li v-for="ep in content.episodeList">
                        <RouterLink :to="`/Content/${content.id}/${ep.id}`">{{ ep.name }}</RouterLink>
                    </li>
                </ul>
                <div class="parentof_btn_like_dislike">
                    <button :class="likeOrDislike.currentUserVal == 'Like' ? 'active' : ''"
                        @click="(e) => likeOrDislikeAdd(e, 0)">
                        <FontAwesomeIcon icon="thumbs-up" /> {{ likeOrDislike.likeCount }}
                    </button>
                    <button :class="likeOrDislike.currentUserVal == 'Dislike' ? 'active' : ''"
                        @click="(e) => likeOrDislikeAdd(e, 1)">
                        <FontAwesomeIcon icon="thumbs-down" /> {{ likeOrDislike.dislikeCount }}
                    </button>
                </div>

            </div>

            <div class="col-12 mt-4">
                <div class="content_title_parent">
                    <h3 class="m-0">Açıklama</h3>
                </div>
            </div>
            <img :src="API_URL + content.imageLink" class="col-lg-2 mt-2 poster" alt="">
            <div class="col-lg-8 mt-2">
                <div class="list_of_content_values mb-2">
                    <button v-if="content.categories" v-for="i in Object.keys(content.categories)" class="btn_theme">
                        <FontAwesomeIcon icon="tag"></FontAwesomeIcon> {{ content.categories[i] }}
                    </button>
                    <button class="btn_theme">
                        <FontAwesomeIcon icon="eye" /> 300
                    </button>
                    <button class="btn_theme">
                        <FontAwesomeIcon icon="comments" /> 300
                    </button>
                </div>
                <div class="content_description">
                    {{ content.description }}
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
                            <img :src="USER.imageLink" alt="">
                            <Textbox class="content_text" type="textarea" placeholder="Yorum Giriniz"
                                v-model="currentComment.text" />
                            <div class="w-100 text-end">
                                <button class="submit_btn mt-2" @click="sendComment">Gönder</button>
                            </div>
                            <div v-if="!isUser()" class="login_blur"> <button class="btn_theme"
                                    @click="() => { router.push('/User/Login') }">Giriş
                                    Yap</button></div>
                        </div>
                        <div v-for="i in comments">
                            <RouterLink :to="'/user/' + i.userName"><img :src="i.imageLink" alt=""></RouterLink>
                            <div class="content_text">
                                <div class="title">{{ i.userName }}</div>
                                <span v-if="!i.isOverflow" class="content_text">
                                    {{ i.text }}
                                </span>
                                <span v-else class="">
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
    </div>
</template>
<script>
export default {
    data() {
        return {
            comments: [],
            content: new Content(),
            currentEpisode: new Episode({}),
            currentComment: new Comment(),
            likeOrDislike: {
                currentUserVal: null,
                dislikeCount: null,
                likeCount: null
            }
        }
    },
    methods: {
        resizeEpisodeNode() {
            var doit;
            var resizedw = () => {
                document.querySelector(".list_of_episode").style.height = (this.$refs.tabcontent.offsetWidth - 24) / 1.77 + "px";
            }
            resizedw()
            window.onresize = function () {
                clearTimeout(doit);
                doit = setTimeout(resizedw, 100);
            };
        },
        async likeOrDislikeAdd(e, param) {
            this.likeOrDislike.currentUserVal = param;
            var result = (await axios.postForm("like/CUD", { episodeId: this.currentEpisode.id, likeOrDislike: param }));
            if (result.status == 200) {
                this.likeOrDislike = result.data;
            }
        },
        changeLabel(e, src) {
            $(".tabs_label > label").removeClass("focus");
            e.target.classList.add("focus")
            $(".tab_content > .tab").addClass("d-none")
            $(`.tab_content > #src${src.id}.tab`).removeClass("d-none")
        },
        async refreshModel() {
            var req = (await axios.get(`Content/Get/${this.$route.params.id}`));
            if (req.status == 200) {
                this.content = req.data;
            }
            this.currentEpisode = this.$route.params.episodeId ? this.content.episodeList.filter(x => x.id == this.$route.params.episodeId)[0] : this.content.episodeList[0];
            req= (await axios.get("like/get?episodeId=" + this.currentEpisode.id));
            if(req.status==200){
            this.likeOrDislike =req.data
        }
            this.comments = [];
            req=await axios.get(`Comment/getlist?episodeId=${this.currentEpisode.id}`);
            if(req==200){
            req.data.forEach(x => {
                this.comments.push(new Comment(x));
            })
        }
            this.currentComment.episodeId = this.currentEpisode.id;
            if (this.USER != null) {
                this.currentComment.imageLink = this.USER.imageLink;
            }
            if (this.currentEpisode?.id) {
                axios.get("content/watch/" + this.currentEpisode.id)
            }
        },
        async sendComment() {
           var req= await axios.postForm("Comment/Create", this.currentComment);
           if(req.status==200){
            this.comments.unshift(new Comment(this.currentComment));
            this.currentComment = new Comment();
            this.currentComment.imageLink = this.USER.imageLink;
        }
        }
    },
    created() {
        this.refreshModel();
    },
    mounted() {
        this.resizeEpisodeNode();
    },
    unmounted() {
        window.onresize = null;
    },
    watch: {
        $route(from, to) {
            this.refreshModel();
        }
    }
}
</script>
<style scoped>
.parentof_btn_like_dislike {
    border: 1px solid var(--pri-border-color);
    width: max-content;
    border-radius: 26px;
    padding: 5px;
    margin: auto;
}

.parentof_btn_like_dislike>button {
    padding-inline: 8px;
}

.parentof_btn_like_dislike>button:where(:hover, .active) {
    color: var(--pri-btn-color);
}

.parentof_btn_like_dislike>button:first-child {
    border-right: 1px solid var(--pri-border-color);
}

.list_of_comment>div {
    display: flex;
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

.list_of_comment>div img {
    width: 50px;
    height: 50px;
    object-fit: cover;
    border-radius: 100%;
    margin: 5px;
}

.list_of_comment>div>.content_text {
    width: calc(100% - 70px);
}

.list_of_comment>div>.content_text>.title {
    opacity: 0.6;
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