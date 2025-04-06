<template>
  <div>
    <h5>Выдать книгу</h5>

    <template v-if="$store.state.selectedGiveBookToReaderBook">
      <div class="row">
        <div class="col-12">
          <h4>
            {{ $store.state.selectedGiveBookToReaderBook.name }},
            {{ $store.state.selectedGiveBookToReaderBook.author }},
            {{ $store.state.selectedGiveBookToReaderBook.yearPublication }} года
          </h4>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col-12">
          Кому выдать:
          <select
            v-model="readerId"
            class="form-select"
            @change="changeReaderId"
          >
            <option
              :value="reader.id"
              v-for="reader in $store.state.readerOptions"
              :key="reader.id"
              :disabled="loadReaderInfo"
            >
              {{ reader.name }}, {{ DD_MM_YYYY(reader.dateBirth) }} г.
            </option>
          </select>
        </div>
      </div>

      <div v-if="loadReaderInfo">
        <div class="row">
          <div class="col-12">Загрузка данных читателя...</div>
        </div>
      </div>
      <div v-else>
        <div v-if="$store.state.selectedReader">
          <div class="row">
            <div class="col-12">
              <h4>Ранее выданные книги:</h4>
            </div>
          </div>
          <div v-if="$store.state.selectedReader.issuedBooks.length == 0">
            У читателя нет выданных книг
          </div>
          <div v-else>
            <div
              class="row border-bottom-1"
              v-for="(issuedBook, index) in $store.state.selectedReader
                .issuedBooks"
              :key="issuedBook.id + '_' + index"
            >
              <div class="col-12">
                {{ index + 1 }}) {{ issuedBook.book.name }},
                {{ issuedBook.book.author }},
                {{ issuedBook.book.yearPublication }} года,
                {{ issuedBook.count }} шт. выдано
                {{ DD_MM_YYYY(issuedBook.dateOfIssue) }} числа
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="row mt-4">
        <div class="col-auto">
          <button
            class="btn btn-sm btn-success me-3"
            :disabled="isSaveForm || !readerId"
            @click="save"
          >
            Выдать книгу
          </button>
          <button
            class="btn btn-sm btn-secondary"
            @click="cancel()"
            :disabled="isSaveForm"
          >
            Отмена
          </button>
        </div>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { GiveBookToReaderDto } from "@/models/dto/give-book-to-reader-dto"
import moment from "moment"
import { defineComponent } from "vue"

export default defineComponent({
  name: "GiveBookToReader",
  components: {},
  data() {
    return {
      isSaveForm: false,
      readerId: null,
      loadReaderInfo: false,
    }
  },
  methods: {
    async save(): Promise<void> {
      this.isSaveForm = true

      await this.$store.dispatch("giveBookToReader", {
        bookId: this.$store.state.selectedGiveBookToReaderBook.id,
        readerId: this.readerId,
      })

      this.isSaveForm = false

      this.$store.commit("setBooksTab", "AvailableBooks")
    },
    async changeReaderId(): Promise<void> {
      this.loadReaderInfo = true
      await this.$store.dispatch("getReaderById", this.readerId)
      this.loadReaderInfo = false
    },
    DD_MM_YYYY(dateBirth: Date | string) {
      return moment(dateBirth).format("DD.MM.YYYY")
    },
    cancel(): void {
      this.$store.commit("setBooksTab", "AvailableBooks")
    },
  },
  async mounted(): Promise<void> {
    this.readerId = null
    this.$store.commit("setSelectedReader", null)
  },
})
</script>
