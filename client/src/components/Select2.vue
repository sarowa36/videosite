<script setup>
defineEmits(["update:modelValue"])
</script>

<template>
  <select ref="select" class="select2 js-example-basic-single" >
    <slot></slot>
  </select>
</template>
<style>
select.select2:not(.select2-hidden-accessible) {
  height: 0;
}
</style>
<script>
export default {
  data() {
    return {
    }
  },
  methods:{
    onchange(){
      var node = this.$refs.select;
      this.value=$(node).val();
    }
  },
  mounted() {
    var node = this.$refs.select;
    if (!$(node).hasClass("select2-hidden-accessible")) {
      $(node).select2({ theme: "classic", width: '100%' });
      $(node).on("change.ss", this.onchange)
    }
  },
  beforeUnmount() {
    var node = this.$refs.select;
    $(node).off().select2("destroy")
  },
  watch:{
    "value"(newVal,oldVal){
      var deepClonedNewVal=newVal ? JSON.parse(JSON.stringify(newVal)):undefined;
      var deepClonedOldVal=oldVal ? JSON.parse(JSON.stringify(oldVal)) :undefined;
      const node =this.$refs.select;
      $(node).off(".ss")
      $(node).val(this.modelValue).trigger("change")
      $(node).on("change.ss",this.onchange)
    }
  },
  computed:{
    value:{
      get(){
        return this.modelValue;
      },
      set(val){
        this.$emit("update:modelValue",val);
      }
    }
  },
  props: ["modelValue"]
}
</script>