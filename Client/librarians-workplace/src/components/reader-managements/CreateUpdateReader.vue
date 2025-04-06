<template>
  <div>
    <template v-if="!formLoad">
      <template v-if="isUpdate">
        <h5>Редактирование читателя</h5>
      </template>
      <template v-else>
        <h5>Добавить читателя</h5>
      </template>
      <div class="mb-3">
        <label for="reader_FullName" class="form-label">ФИО</label>
        <input
          id="reader_FullName"
          type="text"
          class="form-control form-control-sm"
          v-model.trim="fullName"
        />
        <div
          id="reader_FullName_error"
          class="form-text text-danger"
          v-if="isFullNameError"
        >
          Заполните ФИО
        </div>
      </div>

      <div class="row mb-3">
        <div class="col-3">
          <VueDatePicker
            v-model="dateBirth"
            locale="ru"
            :enableTimePicker="false"
          ></VueDatePicker>
          <div
            id="reader_FullName_error"
            class="form-text text-danger"
            v-if="isDateBirthError"
          >
            Заполните дату с 1900 г. по 2010г.
          </div>
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
import { CreateReaderDto } from "@/models/dto/readers/create-reader-dto"
import { UpdateReaderVm } from "@/models/view-models/update-reader-vm"
import VueDatePicker from "@vuepic/vue-datepicker"
import "@vuepic/vue-datepicker/dist/main.css"
import moment from "moment"

export default defineComponent({
  name: "CreateUpdateReader",
  components: { VueDatePicker },
  data() {
    return {
      fullName: "",
      isFullNameError: false,
      dateBirth: null,
      isDateBirthError: false,

      isUpdate: false,
      formLoad: true,
      isSaveForm: false,
    }
  },
  methods: {
    async save(): Promise<void> {
      if (!this.validationForm()) return

      let dateBirth = moment(this.dateBirth).format("YYYY-MM-DD")
      this.isSaveForm = true
      if (this.isUpdate) {
        await this.$store.dispatch("updateReader", {
          id: this.$store.state.selectedReader.id,
          dto: {
            fullName: this.fullName,
            dateBirth: dateBirth
          },
        } as UpdateReaderVm)
      } else {
        await this.$store.dispatch("createReader", {
          fullName: this.fullName,
          dateBirth: dateBirth
        } as CreateReaderDto)
      }

      this.isSaveForm = false

      this.$store.commit("setReaderManagementTab", "list")
    },
    // максимально простая валидация
    validationForm(): boolean {
      let result = true
      if (!this.fullName) {
        result = false
        this.isFullNameError = true
      } else {
        this.isFullNameError = false
      }

      const isBeforeDate = !moment(this.dateBirth).isBefore(moment([2010, 1, 1]))
      const isAfterDate = !moment(this.dateBirth).isAfter(moment([1900, 1, 1]))

      if (!this.dateBirth || isBeforeDate || isAfterDate) {
        result = false
        this.isDateBirthError = true
      } else {
        this.isDateBirthError = false
      }

      return result
    },
    cancel(): void {
      this.$store.commit("setReaderManagementTab", "list")
    },
  },
  async mounted(): Promise<void> {
    this.formLoad = true
    this.isSaveForm = false
    if (this.$store.state.selectedReaderOption) {
      this.isUpdate = true

      await this.$store.dispatch(
        "getReaderById",
        this.$store.state.selectedReaderOption.id
      )

      this.fullName = this.$store.state.selectedReader.fullName
      this.dateBirth = this.$store.state.selectedReader.dateBirth
    } else {
      this.isUpdate = false

      this.fullName = ""
      this.dateBirth = null
    }

    this.formLoad = false
  },
})
</script>
