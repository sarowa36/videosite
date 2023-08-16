<script setup>
import ListedContent from '../components/ListedContent.vue';
import Select2 from "../components/Select2.vue";
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import axios from "axios";
import { Content } from '../models/Content';
import router from '../router';
</script>

<template>
  <div class="container mt-3 mb-3">
    <div ref="row" class="row">
      <div class="col-12 filter">
        <div class="filter_bar">
          <div class="filter_list">
            <div v-if="filter.category" @click="removeFilter('category')">
              <FontAwesomeIcon icon="xmark"></FontAwesomeIcon> <strong>Kategori:</strong> {{ filter.category.toString() }}
            </div>
            <div v-if="filter.keyword" @click="removeFilter('keyword')">
              <FontAwesomeIcon icon="xmark"></FontAwesomeIcon> <strong>Anahtar Kelime:</strong> {{ filter.keyword }}
            </div>
            <div v-if="filter.order" @click="removeFilter('order')">
              <FontAwesomeIcon icon="xmark"></FontAwesomeIcon> <strong>Sırala:</strong> {{ filter.order }}
            </div>
            <div v-if="filter.release" @click="removeFilter('release')">
              <FontAwesomeIcon icon="xmark"></FontAwesomeIcon> <strong>Yayın Tarihi:</strong> {{ filter.release }}
            </div>
          </div>
          <button class="btn_theme" @click="toggleFilterBar"> <font-awesome-icon icon="filter" /></button>
        </div>
        <div class="filter_row row mb-2 justify-content-end" style="display: none;">
          <div class="col-md-3">
            <label>Kategori Seçiniz</label>
            <select2 class="category_select" :searchbox="true" multiple="" v-model="filter.category">
              <option value="Horror">Horror</option>
              <option value="Comedy">Comedy</option>
            </select2>
          </div>
          <div class="col-md-3">
            <label>Çıkış Yılı</label>
            <select2 class="release_year_select" v-model="filter.release">
              <option v-for="i in releaseDateYears()" :value="i">{{ i }}</option>
            </select2>
          </div>
          <div class="col-md-3">
            <label>Anahtar Kelime</label>
            <input type="text" class="w-100" v-model="filter.keyword">
          </div>
          <div class="col-md-3">
            <label>Sıralama Ölçeği</label>
            <Select2 class="order_select" v-model="filter.order">
              <option value="recommend">Önerilenler</option>
              <option value="most_view">En Çok İzlenenler</option>
              <option value="most_like">En Çok Beğenilenler</option>
              <option value="most_save">En Çok Kaydedilenler</option>
            </Select2>

          </div>
          <div class="col-md-3 mt-2 text-end">
            <button class="btn_theme" @click="setFilter">
              <FontAwesomeIcon icon="search"></FontAwesomeIcon> Ara
            </button>
          </div>
        </div>
      </div>
      <ListedContent v-for="content in contents" v-bind:key="content.id" :content="content"></ListedContent>
    </div>
  </div>
</template>
<style>
.filter_bar {
  width: 100%;
  padding: 5px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.filter {
  background-color: var(--tri-color);
  position: sticky;
  top: 15px;
}

.filter.shadow {
  background-color: transparent;
  backdrop-filter: blur(10px);
}

.filter_row {
  border-top: 1px var(--sec-border-color) dashed;
  transition: initial;
}

.filter_list {
  display: flex;
  flex-wrap: wrap;
}

.filter_list>div {
  background-color: var(--tri-btn-color);
  padding: 5px 8px;
  font-size: 15px;
  margin-right: 5px;
}
</style>
<script>
export default {
  data() {
    return {
      filter: {
        order: "",
        keyword: "",
        category: [],
        release: ""
      },
      contents: [new Content()],
      lazyLoadObserver: new IntersectionObserver(()=>{},{}),
      updateFinishedEvents: []
    }
  },
  methods: {
    releaseDateYears() {
      var currentYear = (new Date()).getFullYear();
      var ary = [];
      for (var i = 1900; i < currentYear; i++) {
        ary.push(i);
      }
      ary.push("Hepsi")
      ary.reverse();
      return ary;
    },
    toggleFilterBar(e) {
      $(".filter_row").animate({ height: "toggle" })
    },
    removeFilter(filterName) {
      if (Array.isArray(this.filter[filterName])) {
        this.filter[filterName] = [];
      }
      else {
        this.filter[filterName] = "";
      }
    },
    setFilter(){
      router.push({query:this.filter})
    },
    async getData(id) {
      if (!id) {
        this.contents = [];
        this.lazyLoadObserver = new IntersectionObserver(([e]) => {
          if(e.isIntersecting){
            this.lazyLoadObserver.unobserve(e.target);
            this.getData(this.contents[this.contents.length-1].id)
          }
        }, {
          rootMargin: "0px 0px",
          threshold: [1]
        });
      }
      var {data}=(await axios.get("content/getlist/" + id));
      data.forEach(element => {
        this.contents.push(new Content(element));
      });
      this.updateFinishedEvents.push(() => {
        var node = this.$refs.row.children[this.$refs.row.children.length-5]
        this.lazyLoadObserver.observe(node)
      })
    }
  },
  mounted() {
    this.getData()
    const observer = new IntersectionObserver(
      ([e]) => {
        e.target.classList.toggle("shadow", e.intersectionRatio < 1);
      },
      {
        rootMargin: "-16px 0px",
        threshold: [1]
      });
    observer.observe(document.querySelector(".filter"));
  },
  updated() {
    if (this.updateFinishedEvents.length > 0) {
      this.updateFinishedEvents.forEach(element => {
        if (typeof (element) == "function") {
          element()
        }
      });
      this.updateFinishedEvents = [];
    }
    
  }
}
</script>