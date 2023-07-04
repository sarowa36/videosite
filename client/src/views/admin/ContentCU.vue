<script setup>
import EditLayout from '../../components/EditLayout.vue';
import Textbox from '../../components/Textbox.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>
<template>
    <EditLayout user_role="admin">
        <div class="row justify-content-end">
            <Textbox class="col-12" placeholder="Name" />
            <Textbox class="col-12" placeholder="Açıklama" type="textarea" />
            <div class="mt-4 mb-2">Bölüm ve Kaynak Düzenleme</div>
            <div class="col-5">
                <ul class="episode_and_src_list">
                    <li v-for="ep in episodes" :key="ep.id">
                        <div @click="(e) => selectVal(e, ep)">{{ ep.name }}</div><a href="#" @click="openSrcMenu">
                            <FontAwesomeIcon icon="arrow-down" />
                        </a>
                        <ul style="display: none;">
                            <li v-for="src in ep.src" @click="(e) => selectVal(e, src)">{{ src.name }}</li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="col-2 d-flex flex-column align-center">
                <button class="btn_theme mt-1" @click="addEpisode">
                    Yeni Bölüm Ekle
                </button>
                <button v-if="selected.src!=null" class="btn_theme mt-1" @click="addSource">
                  Yeni Kaynak Ekle
                </button>
                <button class="btn_theme mt-1" @click="removeSelected">
                  Sil
                </button>
            </div>
            <div class="col-5 list_of_src">
                <Textbox v-if="selected.name !=null" v-model="selected.name" placeholder="İsim" />
                <Textbox v-if="selected.iframe !=null" v-model="selected.iframe" placeholder="İframe Kodu" type="textarea" height="150px" />
            </div>
            <div class="mt-4"></div>
            <div class="col-4">
                <img src="/src/assets/poster.webp" alt="">
            </div>
            <div class="col-8">
                <Textbox type="file" accept="image/*" placeholder="Poster Yükleyiniz" id="poster" />
            </div>
            <div class="col-2">
                <button class="btn_theme">Kaydet</button>
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
.episode_and_src_list>li>a *{
    pointer-events: none;
}
.episode_and_src_list>li>ul {
    padding: 0;
    width: 100%;
    transition: initial;
}
.episode_and_src_list>li>ul>li{
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
            episodes: [],
            selected: {}
        }
    },
    async mounted() {
        this.episodes = await $.ajax(this.API_URL + "Episodes.json");
    },
    methods: {
        selectVal(event, val) {
            document.querySelectorAll(".episode_and_src_list .active").forEach(element => { element.classList.remove("active") });
            event.originalTarget.classList.add("active");
            this.selected={};
            this.selected = val
        },
        removeSelected(){
           if(this.selected.iframe){
            this.episodes.filter((x)=>x.id==this.selected.episode_id)[0].src= this.episodes.filter((x)=>x.id==this.selected.episode_id)[0].src.filter(x=>x.id!=this.selected.id)
           }
           else{
            this.episodes=this.episodes.filter(x=>x.id!=this.selected.id);
           }
           this.selected={};
        },
        addEpisode(){
            var newEp={id:-1,name:"Yeni Bölüm", src:[]};
            this.episodes.push(newEp);
            this.selected=newEp;
        },
        addSource(){
            var newSrc={id:-1,name:"Yeni Kaynak",episode_id:this.selected.id, iframe:""}
            this.selected.src.push(newSrc);
            this.selected=newSrc;
        },
        openSrcMenu(event){
            const node= event.originalTarget;
            event.preventDefault();
            if(node.style.rotate==""){
                node.style.rotate="180deg"
            }
            else{
                node.style.rotate="";
            }
            $(node.nextElementSibling).animate({height:"toggle"});
        }
    }
}
</script>
