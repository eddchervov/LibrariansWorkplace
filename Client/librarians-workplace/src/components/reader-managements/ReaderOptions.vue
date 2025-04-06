<template>
  <div>
    <button class="btn btn-sm btn-secondary mb-3" @click="addReader()">
      Добавить читетеля
    </button>

    <table class="table table-hover">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Название</th>
          <th scope="col">Дата рождения</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr colspan="6" v-if="$store.state.readerOptions.length == 0">
          Нет читателей
        </tr>
        <tr
          v-for="(reader, index) in $store.state.readerOptions"
          :key="reader.id"
        >
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ reader.name }}</td>
          <td>{{ DD_MM_YYYY(reader.dateBirth) }}</td>
          <td>
            <button
              class="btn btn-sm btn-danger float-end"
              :disabled="btnDeleteReader"
              @click="deleteReader(reader)"
            >
              Удалить
            </button>
            <button
              class="btn btn-sm btn-secondary float-end me-2"
              @click="updateReader(reader)"
            >
              Редактировать
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue"
import { ReaderOption } from "@/models/view-models/reader-option"
import moment from "moment"

export default defineComponent({
  name: "ReaderOptions",
  components: {},
  data() {
    return {
      btnDeleteReader: false,
    }
  },
  methods: {
    DD_MM_YYYY(dateBirth: Date | string) {
      return moment(dateBirth).format('DD.MM.YYYY')
    },
    addReader(): void {
      this.$store.commit("setSelectedReaderOption", null)
      this.$store.commit("setReaderManagementTab", "create")
    },
    updateReader(reader: ReaderOption): void {
      this.$store.commit("setSelectedReaderOption", reader)
      this.$store.commit("setReaderManagementTab", "update")
    },
    async deleteReader(reader: ReaderOption): Promise<void> {
      const result = confirm(`Удалить читателя ${reader.name}?`)
      if (!result) return
      this.btnDeleteReader = true
      await this.$store.dispatch("deleteReader", reader.id)
      this.btnDeleteReader = false
    },
  },
})
</script>
