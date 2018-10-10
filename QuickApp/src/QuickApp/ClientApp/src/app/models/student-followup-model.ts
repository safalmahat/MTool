
export class StudentFollowUpModel {
    constructor(id?: number, FollowUpDate?: string, FollowUpNote?: string, StudentId?: number, UserId?: string) {
        this.id = id;
        this.FollowUpDate = FollowUpDate;
        this.FollowUpNote = FollowUpNote;
        this.StudentId = StudentId;
        this.UserId = UserId;
    }

    public id: number;
    public FollowUpDate: string;
    public FollowUpNote: string;
    public StudentId: number;
    public UserId: string;
}
