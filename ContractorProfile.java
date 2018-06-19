package com.sidalitechnologies.realestate;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.AsyncTask;
import android.provider.MediaStore;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.sidalitechnologies.Prefrences.PrefManager;
import com.sidalitechnologies.Util.AppConstants;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.SoapFault;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;
import org.xmlpull.v1.XmlPullParserException;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ExecutionException;

public class ContractorProfile extends AppCompatActivity {
    final private int REQUEST_CODE_ASK_PERMISSIONS = 123;
    protected static final int REQUEST_CODE_MANUAL = 5;
    Button btnsave;
    Spinner spinnerarea,spinnermatone,spinnermattwo;
    EditText edtcname, edtcaddress, edtcphone, edtcnameceo, edtcphoneceo, edtcdes;
    String comname,comaddress,comphone,comnameceo,comphoneceo,comdes,comuname,cat,picbytes,mats;
    TextView tvuname, tvucell, tvumail;
    String response, response2,response3;
    ImageView imvprofile;
    Bitmap bmImg;
    String Name,Mail,Cell;
    List<String> mylist,mylist2;
    ArrayAdapter arrayAdapter,arrayadapter2;
    String val,valtwo;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_contractor_profile);
        btnsave = (Button) findViewById(R.id.btnciscsave);
        spinnerarea = (Spinner) findViewById(R.id.spinciscarea);
        spinnermatone=(Spinner)findViewById(R.id.spinermatone);
        spinnermattwo=(Spinner)findViewById(R.id.spinermattwo);
        tvuname = (TextView) findViewById(R.id.tvucisname);
        tvucell = (TextView) findViewById(R.id.tvuciscell);
        tvumail = (TextView) findViewById(R.id.tvucismail);
        edtcname = (EditText) findViewById(R.id.edtciscname);
        edtcaddress = (EditText) findViewById(R.id.edtciscaddress);
        edtcphone = (EditText) findViewById(R.id.edtciscphone);
        edtcnameceo = (EditText) findViewById(R.id.edtciscnameceo);
        edtcphoneceo = (EditText) findViewById(R.id.edtciscphoneceo);
        edtcdes = (EditText) findViewById(R.id.edtciscdes);
        imvprofile = (ImageView) findViewById(R.id.imvcprofile);
        imvclick();
        getprevdata();
        getAllAreas();
        getselectarea();
        saveprofile();
        getAllMaterials();
        getselectedmats();
        getPrevDataTwo();
    }
    private void getPrevDataTwo(){
        Bundle extras=getIntent().getExtras();
        if (extras!=null){
            valtwo=extras.getString("foredit");
            if (valtwo.equals("yes")){
                btnsave.setText("Update Profile");

            }
        }
    }
    private void imvclick() {
        imvprofile.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                galleryPermissionDialog();
            }
        });

    }
    void openGallry() {
        Intent photoPickerIntent = new Intent(Intent.ACTION_PICK);
        photoPickerIntent.setType("image/*");
        startActivityForResult(photoPickerIntent, 1);
    }
    void galleryPermissionDialog() {

        int hasWriteContactsPermission = ContextCompat.checkSelfPermission(ContractorProfile.this,
                android.Manifest.permission.READ_EXTERNAL_STORAGE);
        if (hasWriteContactsPermission != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(ContractorProfile.this,
                    new String[]{android.Manifest.permission.READ_EXTERNAL_STORAGE},
                    REQUEST_CODE_ASK_PERMISSIONS);
            return;

        } else {
            openGallry();
        }
    }
    public void showMessageOKCancel(String message, DialogInterface.OnClickListener okListener) {
        new AlertDialog.Builder(ContractorProfile.this)
                .setTitle(R.string.app_name)
                .setMessage(message)
                .setCancelable(false)
                .setPositiveButton("OK", okListener)
                .setNegativeButton("Cancel", null)
                .create()
                .show();
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        switch (requestCode) {
            case REQUEST_CODE_ASK_PERMISSIONS: {
                Map<String, Integer> perms = new HashMap<String, Integer>();
                // Initial
                perms.put(android.Manifest.permission.READ_EXTERNAL_STORAGE, PackageManager.PERMISSION_GRANTED);
                // Fill with results
                for (int i = 0; i < permissions.length; i++)
                    perms.put(permissions[i], grantResults[i]);
                // Check for READ_EXTERNAL_STORAGE

                boolean showRationale = false;
                if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.M) {
                    if (perms.get(android.Manifest.permission.READ_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED) {
                        // All Permissions Granted
                        galleryPermissionDialog();
                    } else {
                        showRationale = ActivityCompat.shouldShowRequestPermissionRationale(this, android.Manifest.permission.READ_EXTERNAL_STORAGE);
                        if (showRationale) {
                            showMessageOKCancel("Read Storage Permission required for this app ",
                                    new DialogInterface.OnClickListener() {
                                        @Override
                                        public void onClick(DialogInterface dialog, int which) {
                                            galleryPermissionDialog();

                                        }
                                    });
                        } else {
                            showMessageOKCancel("Read Storage Permission required for this app ",
                                    new DialogInterface.OnClickListener() {
                                        @Override
                                        public void onClick(DialogInterface dialog, int which) {
                                            Toast.makeText(ContractorProfile.this, "Please Enable the Read Storage permission in permission", Toast.LENGTH_SHORT).show();
                                            Intent intent = new Intent(Settings.ACTION_APPLICATION_DETAILS_SETTINGS);
                                            Uri uri = Uri.fromParts("package", getPackageName(), null);
                                            intent.setData(uri);
                                            startActivityForResult(intent, REQUEST_CODE_MANUAL);
                                        }
                                    });

                            //proceed with logic by disabling the related features or quit the app.
                        }


                    }


                } else {
                    galleryPermissionDialog();

                }

            }
            break;
            default:
                super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent imageReturnedIntent) {
        super.onActivityResult(requestCode, resultCode, imageReturnedIntent);

        switch (requestCode) {
            case 1:
                if (resultCode == RESULT_OK) {
                    try {
                        final Uri imageUri = imageReturnedIntent.getData();
                   /* final InputStream imageStream = getContentResolver().openInputStream(imageUri);
                    final Bitmap selectedImage = BitmapFactory.decodeStream(imageStream);
                    img_profile.setImageBitmap(selectedImage);*/
                        String[] filePathColumn = {MediaStore.Images.Media.DATA};
                        Cursor cursor = getContentResolver().query(imageUri, filePathColumn, null, null, null);
                        cursor.moveToFirst();
                        int columnIndex = cursor.getColumnIndex(filePathColumn[0]);
                        String picturePath = cursor.getString(columnIndex);
                        cursor.close();
                        bmImg = BitmapFactory.decodeFile(picturePath);
                        imvprofile.setImageBitmap(bmImg);
                        //  pictureFile = saveBitmapToFile(new File(picturePath));
                        //Picasso.with(getApplicationContext()).load(new File(picturePath)).transform(new CircleTransform()).into(imgProfile);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
        }
    }
    private void getprevdata(){
        PrefManager prefManager=new PrefManager(this);
        Name=prefManager.getusername();
        Cell=prefManager.getcell();
        Mail=prefManager.getemail();

        tvuname.setText(Name);
        tvumail.setText(Mail);
        tvucell.setText(Cell);
    }
    private void getAllAreas(){
        mylist=new ArrayList<String>();
        try {
            response=new GetAreas().execute().get();
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (ExecutionException e) {
            e.printStackTrace();
        }
        String[] arr=response.split(",");
        for (int i=0;i<arr.length;i++){
            String areaname=arr[i];
            mylist.add(areaname);
        }
        arrayAdapter=new ArrayAdapter(this,R.layout.support_simple_spinner_dropdown_item,mylist);
        spinnerarea.setAdapter(arrayAdapter);
    }
    public static String convert(Bitmap bitmap)
    {
        ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.PNG, 100, outputStream);

        return Base64.encodeToString(outputStream.toByteArray(), Base64.DEFAULT);
    }
    private void getselectedmats(){
        spinnermatone.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                mats=parent.getItemAtPosition(position).toString();

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                Toast.makeText(ContractorProfile.this, "Please select item", Toast.LENGTH_SHORT).show();
            }
        });
    }
    private void saveprofile(){
        btnsave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                comname=edtcname.getText().toString();
                comaddress=edtcaddress.getText().toString();
                comphone=edtcphone.getText().toString();
                //cat;
                comnameceo=edtcnameceo.getText().toString();
                comphoneceo=edtcphoneceo.getText().toString();
                comdes=edtcdes.getText().toString();
                comuname=tvuname.getText().toString();
//                picbytes="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";
                picbytes=convert(bmImg);
                try {
                    response2=new SaveProfile().execute(new String[]{comname,comaddress,comphone,cat,comnameceo,comphoneceo,comdes,comuname,picbytes,mats,valtwo}).get();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (ExecutionException e) {
                    e.printStackTrace();
                }
                if (response2!=null){
                    Log.e("responseSaveDistProfile",response2);
                    if (response2.equals("Inserted")){
                        Toast.makeText(ContractorProfile.this, "Profile Created", Toast.LENGTH_SHORT).show();
                        PrefManager prefManager=new PrefManager(ContractorProfile.this);
                        val="yes";
                        prefManager.setprofileexist(val);
                    }else if (response2.equals("Updated")){
                        Toast.makeText(ContractorProfile.this, "Profile Updated", Toast.LENGTH_SHORT).show();
                    }else{
                        Toast.makeText(ContractorProfile.this, "Error", Toast.LENGTH_SHORT).show();
                    }
                }
            }
        });
    }
    private void getselectarea(){
        spinnerarea.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                cat=parent.getItemAtPosition(position).toString();

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                Toast.makeText(ContractorProfile.this, "Please select item", Toast.LENGTH_SHORT).show();
            }
        });
    }

    private void getAllMaterials(){
        mylist2=new ArrayList<String>();
        try {
            response3=new GetMaterials().execute().get();
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (ExecutionException e) {
            e.printStackTrace();
        }
        String[] arr=response3.split(",");
        for (String areaname : arr) {
            mylist2.add(areaname);
        }
        arrayadapter2=new ArrayAdapter(this,R.layout.support_simple_spinner_dropdown_item,mylist2);
        spinnermatone.setAdapter(arrayadapter2);
        spinnermattwo.setAdapter(arrayadapter2);
    }

    public class GetAreas extends AsyncTask<String, String, String> {
        String AllData;
        private String SOAP_ACTION = "http://tempuri.org/getAllAreas";
        private String SOAP_METHOD_NAME = "getAllAreas";
        private static final String SOAP_NAMESPACE = "http://tempuri.org/";
        private String SOAP_URL = AppConstants.URL+"getAllAreas";
        private int RESPONSE_CODE = 0;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();

        }

        @Override
        protected String doInBackground(String... strings) {
            SoapObject soapObject = new SoapObject(SOAP_NAMESPACE,SOAP_METHOD_NAME);
//            soapObject.addProperty("uname", strings[0]);

            SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
            envelope.dotNet = true;
            envelope.setOutputSoapObject(soapObject);

            HttpTransportSE transport = new HttpTransportSE(SOAP_URL);
            transport.debug = true;
            try {
                transport.call(SOAP_ACTION, envelope);
                SoapObject resultObject=(SoapObject)envelope.bodyIn;
                if (resultObject != null) {
                    AllData= resultObject.getProperty(0).toString();

                    RESPONSE_CODE = 1;
                    resultObject.toString();
                }else {
                    RESPONSE_CODE = 0;


                }
            } catch (XmlPullParserException e) {
                e.getMessage();
                e.printStackTrace();
            } catch (IOException e) {
                e.getMessage();
                e.printStackTrace();
            }
            return AllData;
        }

        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);

        }
    }
    public static class GetMaterials extends AsyncTask<String, String, String> {
        String AllData;
        private String SOAP_ACTION = "http://tempuri.org/GetAllContTypes";
        private String SOAP_METHOD_NAME = "GetAllContTypes";
        private static final String SOAP_NAMESPACE = "http://tempuri.org/";
        private String SOAP_URL = AppConstants.URL+"GetAllContTypes";
        private int RESPONSE_CODE = 0;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();

        }

        @Override
        protected String doInBackground(String... strings) {
            SoapObject soapObject = new SoapObject(SOAP_NAMESPACE,SOAP_METHOD_NAME);
//            soapObject.addProperty("uname", strings[0]);

            SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
            envelope.dotNet = true;
            envelope.setOutputSoapObject(soapObject);

            HttpTransportSE transport = new HttpTransportSE(SOAP_URL);
            transport.debug = true;
            try {
                transport.call(SOAP_ACTION, envelope);
                SoapObject resultObject=(SoapObject)envelope.bodyIn;
                if (resultObject != null) {
                    AllData= resultObject.getProperty(0).toString();

                    RESPONSE_CODE = 1;
                    resultObject.toString();
                }else {
                    RESPONSE_CODE = 0;


                }
            } catch (XmlPullParserException e) {
                e.getMessage();
                e.printStackTrace();
            } catch (IOException e) {
                e.getMessage();
                e.printStackTrace();
            }
            return AllData;
        }

        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);

        }
    }

    public class SaveProfile extends AsyncTask<String, String, String> {
        String AllData;
        private String SOAP_ACTION = "http://tempuri.org/profilecontractor";
        private String SOAP_METHOD_NAME = "profilecontractor";
        private static final String SOAP_NAMESPACE = "http://tempuri.org/";
        private String SOAP_URL = AppConstants.URL+"profilecontractor";
        private int RESPONSE_CODE = 0;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();

        }

        @Override
        protected String doInBackground(String... strings) {
            SoapObject soapObject = new SoapObject(SOAP_NAMESPACE,SOAP_METHOD_NAME);
            soapObject.addProperty("name", strings[0]);
            soapObject.addProperty("address", strings[1]);
            soapObject.addProperty("phone", strings[2]);
            soapObject.addProperty("cat", strings[3]);
            soapObject.addProperty("nameceo", strings[4]);
            soapObject.addProperty("phoneceo", strings[5]);
            soapObject.addProperty("cdes", strings[6]);
            soapObject.addProperty("uid", strings[7]);
            soapObject.addProperty("pic", strings[8]);
            soapObject.addProperty("matone", strings[9]);
            soapObject.addProperty("val", strings[10]);
            SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
            envelope.dotNet = true;
            envelope.setOutputSoapObject(soapObject);

            HttpTransportSE transport = new HttpTransportSE(SOAP_URL);
            transport.debug = true;

            try {
                if (envelope.bodyIn instanceof SoapFault) {
                    String str = ((SoapFault) envelope.bodyIn).faultstring;
                    Log.i("ddddd", str);
                }else {
                    transport.call(SOAP_ACTION, envelope);
                    SoapObject resultObject = (SoapObject) envelope.bodyIn;
                    if (resultObject != null) {
                        AllData = resultObject.getProperty(0).toString();

                        RESPONSE_CODE = 1;
                        resultObject.toString();
                    } else {
                        RESPONSE_CODE = 0;


                    }
                }
            } catch (XmlPullParserException e) {
                e.getMessage();
                e.printStackTrace();
            } catch (IOException e) {
                e.getMessage();
                e.printStackTrace();
            }
            return AllData;
        }

        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);

        }
    }
}
