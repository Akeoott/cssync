<template>
    <nav class="bg-bg-900">
        <div class="container">
            <div class="text-xl p-5 flex justify-left items-center border-b border-primary-dark">
                <ul class="flex space-x-4">
                    <li><router-link class="no-underline font-bold" to="/">cssync</router-link></li>
                    <li><router-link class="no-underline" to="/">Overview</router-link></li>
                    <li><router-link class="no-underline" to="/rclone">Rclone</router-link></li>
                    <li><router-link class="no-underline" to="/settings">Settings</router-link></li>
                    <li>
                        <a class="no-underline" href="https://github.com/Akeoott/cssync" aria-label="Source Code" target="_blank" referrerpolicy="strict-origin-when-cross-origin">
                            Source
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    name: 'CompNavbar',
})
</script>

<style scoped />