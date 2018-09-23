// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

export class StudentEditRegistration {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(Email?: string, PhoneNumber?: string, Address?: string, City?: string, CompletedFaculty?: string, IntrestedFaculty?: string, Percentage?: string) {
  
      this.Email = Email;
      this.PhoneNumber = PhoneNumber;
      this.Address = Address;
      this.City = City;
      this.CompletedFaculty = CompletedFaculty;
      this.IntrestedFaculty = IntrestedFaculty;
      }
  
  
    public Id: string;
    public ChannnedId: number;
    public Token: string;
    public FirstName: string;
    public MiddleName: string;
    public LastName: string;
    public Email: string;
    public PhoneNumber: string;
    public Address: string;
    public City: string;
    public Gender: number;
    public CompletedLevel: string;
    public CompletedFaculty: string;
    public IntrestedFaculty: string;
    public Percentage: string;
  }
  