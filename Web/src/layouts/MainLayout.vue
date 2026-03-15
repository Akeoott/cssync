<template>
    <CompNavbar />
    <main>
        <slot />
    </main>
    <CompFooter />
</template>

<script lang="ts">
import CompFooter from '@/components/CompFooter.vue';
import CompNavbar from '@/components/CompNavbar.vue';
import { defineComponent } from 'vue';

export default defineComponent({
    name: 'MainLayout',
    components: { CompNavbar, CompFooter }
})
</script>

<style scoped />
