import { ReaderBookDto } from "./readers/reader-book-dto";

export interface BookGivenToReaderDto {
  book: ReaderBookDto;
  count: number;
  dateOfIssue: Date;
}
