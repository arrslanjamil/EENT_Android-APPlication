package com.sidalitechnologies.adapter;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RatingBar;
import android.widget.TextView;
import android.widget.Toast;

import com.sidalitechnologies.Util.AppConstants;
import com.sidalitechnologies.Util.Contractor;
import com.sidalitechnologies.realestate.R;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;
import org.xmlpull.v1.XmlPullParserException;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutionException;

/**
 * Created by Anonymous on 4/9/2018.
 */

public class ContractorAdapter extends BaseAdapter {


    private Context context;
    private Contractor datafinal;
    private ViewHolder v;
    public List<Contractor> data;
    Bitmap mybmp=null;
    String trat;
    private ArrayList<Contractor> arraylistt;
    public ContractorAdapter(List<Contractor> selectgroups,Context c){
        context=c;
        data=selectgroups;
        this.arraylistt = new ArrayList<Contractor>();
        this.arraylistt.addAll(data);

    }
    @Override
    public int getCount() {
        return data.size();
    }

    @Override
    public Object getItem(int position) {
        return data.get(position);

    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(final int position, View convertView, ViewGroup parent) {
        View list;
        ListView listView;
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        if (convertView==null){
            list=new View(context);
            list = inflater.inflate(R.layout.gv_layout, null);

            v = new ViewHolder();
            v.name = (TextView) list.findViewById(R.id.tvgvname);
            v.imageView = (ImageView) list.findViewById(R.id.imvgvcont);
            v.address=(TextView)list.findViewById(R.id.tvgvaddress);
            v.desc=(TextView)list.findViewById(R.id.tvgvdesc);
            v.idd=(TextView)list.findViewById(R.id.idcon);
            v.btn=(Button)list.findViewById(R.id.btngvview);
            v.ratingBar=(RatingBar)list.findViewById(R.id.ratingbarcon);

            datafinal = (Contractor) data.get(position);
            v.name.setText(datafinal.getName());
            v.address.setText(datafinal.getAddress());


            try {
                mybmp=new DownloadImage().execute(new String[]{datafinal.getPicpath()}).get();
            } catch (InterruptedException e) {
                e.printStackTrace();
            } catch (ExecutionException e) {
                e.printStackTrace();
            }
            v.imageView.setImageBitmap(mybmp);
            v.desc.setText(datafinal.getDesc());
            v.idd.setText(datafinal.getId());
            try {
                trat=new GetRating().execute(new String[]{v.idd.getText().toString()}).get();
            } catch (InterruptedException e) {
                e.printStackTrace();
            } catch (ExecutionException e) {
                e.printStackTrace();
            }
            if (trat!=null) {
                // need to add formula of rating
                int i=Integer.parseInt(trat);
                float f=(float) i;
                v.ratingBar.setRating(f);
            }
            list.setTag(datafinal);
        }
        else {
            list = (View) convertView;
        }

        return list;
    }
    private static class ViewHolder {
        ImageView imageView;
        TextView name;
        TextView address;
        TextView desc;
        TextView idd;
        Button btn;
        RatingBar ratingBar;
    }
    private class DownloadImage extends AsyncTask<String, Bitmap, Bitmap> {
        @Override
        protected Bitmap doInBackground(String... params) {
            Bitmap myBitmap=null;
            try {
                URL url = new URL(params[0]);
                HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                connection.setDoInput(true);
                connection.connect();
                InputStream input = connection.getInputStream();
                 myBitmap = BitmapFactory.decodeStream(input);
                return myBitmap;
            }catch (Exception e){
                Log.d("DX",e.getMessage());
            }
            return myBitmap;
        }

        @Override
        protected void onPostExecute(Bitmap result) {
//            ImageView imageView = (ImageView) findViewById(ID OF YOUR IMAGE VIEW);
//            imageView.setImageBitmap(result);
        }
    }
    public class GetRating extends AsyncTask<String, String, String> {
        String AllData;
        private String SOAP_ACTION = "http://tempuri.org/getcontractortotalrating";
        private String SOAP_METHOD_NAME = "getcontractortotalrating";
        private static final String SOAP_NAMESPACE = "http://tempuri.org/";
        private String SOAP_URL = AppConstants.URL + "getcontractortotalrating";
        private int RESPONSE_CODE = 0;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();

        }

        @Override
        protected String doInBackground(String... strings) {
            SoapObject soapObject = new SoapObject(SOAP_NAMESPACE, SOAP_METHOD_NAME);
             soapObject.addProperty("uid",strings[0]);

            SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
            envelope.dotNet = true;
            envelope.setOutputSoapObject(soapObject);

            HttpTransportSE transport = new HttpTransportSE(SOAP_URL);
            transport.debug = true;
            try {
                transport.call(SOAP_ACTION, envelope);
                SoapObject resultObject = (SoapObject) envelope.bodyIn;
                if (resultObject != null) {
                    AllData = resultObject.getProperty(0).toString();

                    RESPONSE_CODE = 1;
                    resultObject.toString();
                } else {
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
}
