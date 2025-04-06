<template>
  <div>
    <h4>Управление читателями</h4>
    <hr />

    <template v-if="!isLoadForm">
      <div v-if="$store.state.readerManagementTab === 'list'">
        <ReaderOptions />
      </div>
      <div
        v-if="
          $store.state.readerManagementTab === 'create' ||
          $store.state.readerManagementTab === 'update'
        "
      >
        <CreateUpdateReader />
      </div>
    </template>

    <template v-else>
      <div class="row">
        <div class="col-12 text-center">
          <span class="text-primary">Загрузка...</span>
        </div>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue"
import ReaderOptions from "../components/reader-managements/ReaderOptions.vue"
import CreateUpdateReader from "../components/reader-managements/CreateUpdateReader.vue"

export default defineComponent({
  name: "ReaderManagementView",
  components: {
    ReaderOptions,
    CreateUpdateReader
  },
  data() {
    return {
      isLoadForm: true,
    }
  },
  async mounted(): Promise<void> {
    this.$store.commit('setSelectedReader', null)
    this.$store.commit('setSelectedReaderOption', null)
    this.$store.commit("setReaderManagementTab", "list")
    await this.$store.dispatch("loadReaderManagement")
    this.isLoadForm = false
  },
})
</script>