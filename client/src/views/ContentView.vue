<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
</script>
<template>
    <div class="container content mt-5 mb-5">
        <div class="row">
            <div class="col-12 mb-2">
                <div class="content_title_parent">
                    <h3 class="m-0">Accelerando Datenshi Tachi...</h3>
                </div>
            </div>
            <div class="col-8 tab_content">
                <input type="radio" class="d-none tab_input" id="youtube" name="tabs" checked>
                <div class="tab">
                    <iframe src="https://www.youtube.com/embed/L_LUpnjgPso" title="YouTube video player" frameborder="0"
                        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                        allowfullscreen></iframe>
                </div>
                <input type="radio" class="d-none tab_input" id="src2" name="tabs">
                <div class="tab">
                    <iframe src="https://www.youtube.com/embed/1RMKR8h03iw" title="YouTube video player" frameborder="0"
                        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                        allowfullscreen></iframe>
                </div>
            </div>
            <div class="col-4">
                <ul class="list_of_episode p-0" style="height: 400px;">
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                    <li><a href="#">Bölüm</a></li>
                </ul>
            </div>
            <div class="col-12 ">
                <div class="tabs_label">
                Kaynaklar:
                <label for="youtube" class="focus">youtube</label>
                <label for="src2">src2</label>
                </div>
            </div>
            <div class="col-12 mt-4">
                <div class="content_title_parent">
                    <h3 class="m-0">Açıklama</h3>
                </div>
            </div>
            <img src="../assets/poster.webp" class="col-2 mt-2" alt="">
            <div class="col-8 mt-2">
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
                    Lorem ipsum dolor sit amet consectetur adipisicing elit. Pariatur, voluptates! Suscipit accusantium,
                    nostrum expedita placeat, aliquam impedit officia animi possimus consequatur reiciendis maxime libero
                    dolor nihil earum natus aspernatur et?
                    Minima dolor ipsa non eligendi officiis corporis quam inventore, consequuntur dolorum corrupti molestiae
                    sed veniam libero ipsam. Enim praesentium recusandae consequatur? Officiis repudiandae eaque iste quam
                    quis dignissimos aliquam fugit?
                    Ex praesentium nisi soluta ipsam officia labore molestiae perspiciatis, sint quod autem eum cumque a
                    provident sit sunt consequatur cum culpa aspernatur quidem asperiores aliquid perferendis! Qui molestiae
                    perspiciatis laborum!
                </div>
            </div>
            <div class="col-12 mt-4">
                <div class="content_title_parent">
                    <h3 class="m-0">Yorumlar</h3>
                </div>
                <div class="comment">
                    <div class="create_comment"></div>
                    <div class="list_of_comment">
                        <div>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    methods:{
        resizeEpisodeNode(){
            var doit;
        var resizedw = () => {
            document.querySelector(".list_of_episode").style.height = document.querySelector(".content iframe").offsetHeight + "px";
        }
        resizedw()
        window.onresize = function () {
            clearTimeout(doit);
            doit = setTimeout(resizedw, 100);
        };
        }
    },
    mounted() {
        $(".tabs_label > label").on("click", (e) => {
            $(".tabs_label > label").removeClass("focus");
            e.target.classList.add("focus")
        });
    }
}
</script>
<style scoped>
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

.content {
    background-color: var(--pri-color);
    padding: 30px;
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
.list_of_episode>li:first-child>a{
    border-top: initial;
}
.tab:not(:focus) {
    display: none;
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
}</style>