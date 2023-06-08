<script setup>
defineProps({
    searchbox:{
        type:Boolean,
        default:false
    },
    styleClass:"",
    modelValue:String
})
defineEmits(["update:modelValue"])
</script>

<template>
 <select ref="select" :class="'select2 '+styleClass" :data-minimum-results-for-search="searchbox ? 'Infinity' : null">
    <slot></slot>
 </select>
</template>
<style>
select.select2:not(.select2-hidden-accessible){
  height: 0;
}
</style>
<script>
export default{
    data(){
        return {
        }
    },
    mounted(){
        var node=this.$refs.select;
        if (!$(node).hasClass("select2-hidden-accessible")) {
          $(node).select2({ theme: "classic", width: '100%' });
          $(node).on("change",()=>{
            this.$emit("update:modelValue",$(node).val())
          })
        }
    },
    beforeUnmount() {
    var node=this.$refs.select;
    $(node).off().select2("destroy")
    },

}
</script>