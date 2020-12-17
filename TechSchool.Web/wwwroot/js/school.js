var schoolApp = new Vue({
    el: '#school-app',
    data: {
        message: 'testing...',
        students: [],
        perPage: 10, 
        currentPage: 1,
        fields: [],
        student: null,
        totalRecords: 0
    },
    methods: {
        toastError: function (message, title) {
            let self = this;
            self.$bvToast.toast(message, {
                title: title || 'Error',
                variant: 'danger',
                solid: true
            });
        },
        toastWarning: function (message, title) {
            let self = this;
            self.$bvToast.toast(message, {
                title: title || 'Warning',
                variant: 'warning',
                solid: true
            });
        },
        toastInfo: function (message, title) {
            let self = this;
            self.$bvToast.toast(message, {
                title: title || 'Info',
                variant: 'info',
                solid: true
            });
        },
        toastSuccess: function (message, title) {
            let self = this;
            self.$bvToast.toast(message, {
                title: title || 'Success',
                variant: 'success',
                solid: true
            });
        },
        createStudent: function(){
            let self = this;
            self.student = {
                studentId: 0,
                firstName: '',
                lastName: '',
                city: '',
                phoneNumber: ''
            }
        },
        canSaveStudent:function(){
            let self = this;
            return self.student.firstName && self.student.lastName
            && self.student.city && self.student.phoneNumber;
        },
        cancelStudent: function(){
            let self = this;
            self.student = null;
        },
        saveStudent:function(){
            let self = this;

            axios.post('/api/SchoolApi/CreateStudent', self.student)
            .then(function(res){
                let apiResult = res.data;

                if(apiResult.success){
                    self.toastSuccess("Student created successfully", "Success");
                    self.student = apiResult.data;

                    let newStudents = self.students;
                    newStudents.push(self.student);
                    self.student = null;
                    self.students = newStudents;
                    self.totalRecords = self.students.length;
                    self.sortStudents();
                    // refresh the table.
                    self.$refs.studentsTable.refresh();
                    
                }
                else {
                    self.toastError(apiResult.message, "Error in saving student");
                }
            })
            .catch(function(error){
                self.toastError(error, "Exception in saving student");
            });
        },
        setFieldConfig:function(){
            let self = this;
            self.fields = [
                { key: 'studentId', label: 'Student Id'},
                { key: 'firstName', label: 'First Name'},
                { key: 'lastName', label: 'Last Name' },
                { key: 'city', label: 'City' },
                { key: 'phoneNumber', label: 'Phone Number' },
            ];
        },
        sortStudents:function() {
            let self = this;
            self.students.sort(function(a, b){
                var nameA = a.lastName.toUpperCase(); // ignore upper and lowercase
                var nameB = b.lastName.toUpperCase(); // ignore upper and lowercase
                if (nameA < nameB) {
                    return -1;
                }
                if (nameA > nameB) {
                    return 1;
                }

                // names must be equal
                return 0;
            });
        }
    },
    mounted: function(){
        let self = this;
        self.setFieldConfig();
        axios.get('/api/SchoolApi/Students')
        .then(function(res){
            let apiResult = res.data;
            if(apiResult.success){
                self.students = apiResult.data;
                self.sortStudents();
                self.totalRecords = self.students.length;
            }
        });
    }
});