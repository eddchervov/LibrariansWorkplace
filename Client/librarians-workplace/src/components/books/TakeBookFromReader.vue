<template>
  <div>
    <h5>Принять книгу</h5>

    <template v-if="$store.state.selectedBookFromTabTakeBookFromReader">
      <div class="row">
        <div class="col-12">
          <h4>
            {{ $store.state.selectedBookFromTabTakeBookFromReader.name }},
            {{ $store.state.selectedBookFromTabTakeBookFromReader.author }},
            {{
              $store.state.selectedBookFromTabTakeBookFromReader.yearPublication
            }}
            года
          </h4>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col-12">
          Выбрать читателя:
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
          <div v-if="filterIssuedBooks.length == 0">
            У читателя нет такой книги
          </div>
          <div v-else>
            <div class="row">
              <div class="col-12">
                <table class="table table-hover">
                  <thead>
                    <tr>
                      <th scope="col">#</th>
                      <th scope="col">Название</th>
                      <th scope="col">Автор</th>
                      <th scope="col">Год публикации</th>
                      <th scope="col">Кол-во</th>
                      <th scope="col">Дата выдачи</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr
                      v-for="(issuedBook, index) in filterIssuedBooks"
                      :key="issuedBook.book.id + '_' + index"
                    >
                      <th scope="row">{{ index + 1 }}</th>
                      <td>{{ issuedBook.book.name }}</td>
                      <td>{{ issuedBook.book.author }}</td>
                      <td>{{ issuedBook.book.yearPublication }}</td>
                      <td>{{ issuedBook.count }}</td>
                      <td>{{ DD_MM_YYYY(issuedBook.dateOfIssue) }}</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="row mt-4">
        <div class="col-auto">
          <button
            class="btn btn-success me-3"
            :disabled="isSaveForm || !readerId || filterIssuedBooks.length == 0"
            @click="save()"
          >
            Принять книгу
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
import { BookGivenToReaderDto } from "@/models/dto/book-given-to-reader-dto"
import { ReturnBookToLibraryDto } from "@/models/dto/return-book-to-library-dto"
import moment from "moment"
import { defineComponent } from "vue"

export default defineComponent({
  name: "TakeBookFromReader",
  components: {},
  data() {
    return {
      isSaveForm: false,
      readerId: null,
      loadReaderInfo: false,
    }
  },
  computed: {
    filterIssuedBooks(): BookGivenToReaderDto[] {
      if (
        !this.$store.state.selectedBookFromTabTakeBookFromReader ||
        !this.$store.state.selectedReader
      )
        return []
      return this.$store.state.selectedReader.issuedBooks.filter(
        (x: BookGivenToReaderDto) =>
          x.book.id ==
          this.$store.state.selectedBookFromTabTakeBookFromReader.id
      )
    },
  },
  methods: {
    async save(): Promise<void> {
      if (!this.readerId || this.filterIssuedBooks.length == 0) return

      this.isSaveForm = true

      await this.$store.dispatch("returnBookToLibrary", {
        bookId: this.$store.state.selectedBookFromTabTakeBookFromReader.id,
        readerId: this.readerId,
      })

      this.isSaveForm = false

      this.$store.commit("setBooksTab", "IssuedBooks")
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
      this.$store.commit("setBooksTab", "IssuedBooks")
    },
  },
  async mounted(): Promise<void> {
    this.readerId = null
    this.$store.commit("setSelectedReader", null)
  },
})
</script>
