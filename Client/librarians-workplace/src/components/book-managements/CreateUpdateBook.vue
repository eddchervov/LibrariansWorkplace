<template>
  <div>
    <template v-if="!formLoad">
      <template v-if="isUpdate">
        <h5>Редактирование книги</h5>
      </template>
      <template v-else>
        <h5>Создать книгу</h5>
      </template>
      <div class="mb-3">
        <label for="book_name" class="form-label">Название</label>
        <input
          id="book_name"
          type="text"
          class="form-control form-control-sm"
          v-model.trim="name"
        />
        <div
          id="book_name_error"
          class="form-text text-danger"
          v-if="isNameError"
        >
          Заполните название
        </div>
      </div>
      <div class="mb-3">
        <label for="book_author" class="form-label">Автор</label>
        <input
          id="book_author"
          type="text"
          class="form-control form-control-sm"
          v-model.trim="author"
        />
        <div
          id="book_author_error"
          class="form-text text-danger"
          v-if="isAuthorError"
        >
          Заполните автора
        </div>
      </div>
      <div class="mb-3">
        <label for="book_yearPublication" class="form-label"
          >Год публикации</label
        >
        <input
          id="book_yearPublication"
          type="text"
          class="form-control form-control-sm"
          v-model.number="yearPublication"
        />
        <div
          id="book_yearPublication_error"
          class="form-text text-danger"
          v-if="isYearPublicationError"
        >
          Заполните год публикации от 1 до текущего года
        </div>
      </div>
      <div class="mb-3">
        <label for="book_countCopies" class="form-label">
          Количество копий
        </label>
        <input
          id="book_countCopies"
          type="text"
          class="form-control form-control-sm"
          v-model.number="countCopies"
        />
        <div
          id="book_countCopies_error"
          class="form-text text-danger"
          v-if="isCountCopiesError"
        >
          Заполните количество копий от 1 до 1000000
        </div>
      </div>

      <div class="row">
        <div class="col-auto">
          <button
            class="btn btn-sm btn-success me-3"
            @click="save()"
            :disabled="isSaveForm"
          >
            Сохранить
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
import { CreateBookDto } from "@/models/dto/books/create-book-dto"
import { UpdateBookVm } from "@/models/view-models/update-book-vm"

export default defineComponent({
  name: "CreateUpdateBook",
  components: {},
  data() {
    return {
      name: "",
      isNameError: false,
      author: "",
      isAuthorError: false,
      yearPublication: 0,
      isYearPublicationError: false,
      countCopies: 0,
      isCountCopiesError: false,

      isUpdate: false,
      formLoad: true,
      isSaveForm: false,
    }
  },
  methods: {
    async save(): Promise<void> {
      if (!this.validationForm()) return

      this.isSaveForm = true
      if (this.isUpdate) {
        await this.$store.dispatch("updateBook", {
          id: this.$store.state.selectedBook.id,
          dto: {
            author: this.author,
            name: this.name,
            countCopies: this.countCopies,
            yearPublication: this.yearPublication,
          },
        } as UpdateBookVm)
      } else {
        await this.$store.dispatch("createBook", {
          author: this.author,
          name: this.name,
          countCopies: this.countCopies,
          yearPublication: this.yearPublication,
        } as CreateBookDto)
      }

      this.isSaveForm = false

      this.$store.commit("setBookManagementTab", "list")
    },
    // максимально простая валидация
    validationForm(): boolean {
      let result = true
      if (!this.name) {
        result = false
        this.isNameError = true
      }
      else this.isNameError = false
      if (!this.author) {
        result = false
        this.isAuthorError = true
      }
      else this.isAuthorError = false
      if (
        !this.yearPublication ||
        this.yearPublication < 1 ||
        this.yearPublication > new Date().getFullYear()
      ) {
        result = false
        this.isYearPublicationError = true
      }
      else this.isYearPublicationError = false
      if (
        !this.countCopies ||
        this.countCopies < 1 ||
        this.countCopies > 1000000
      ) {
        result = false
        this.isCountCopiesError = true
      }
      else this.isCountCopiesError = false

      return result
    },
    cancel(): void {
      this.$store.commit("setBookManagementTab", "list")
    },
  },
  async mounted(): Promise<void> {
    this.formLoad = true
    this.isSaveForm = false
    if (this.$store.state.selectedBookOption) {
      this.isUpdate = true

      await this.$store.dispatch(
        "getBookById",
        this.$store.state.selectedBookOption.id
      )

      this.name = this.$store.state.selectedBook.name
      this.author = this.$store.state.selectedBook.author
      this.yearPublication = this.$store.state.selectedBook.yearPublication
      this.countCopies = this.$store.state.selectedBook.countCopies
    } else {
      this.isUpdate = false

      this.name = ""
      this.author = ""
      this.yearPublication = 0
      this.countCopies = 0
    }

    this.formLoad = false
  },
})
</script>
